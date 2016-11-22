using PureCSharpWeb.Framework.Elements;
using PureCSharpWeb.Framework.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PureCSharpWeb.Framework
{
    public class HtmlConverter
    {
        private List<Tuple<string, string>> repeaters = new List<Tuple<string, string>>();
        private Dictionary<string, string> controllerCalls = new Dictionary<string, string>();


        public string Convert(PageBase page)
        {
            repeaters.Clear();
            controllerCalls.Clear();
            using (MemoryStream ms = new MemoryStream())
            {
                XmlWriter writer = XmlWriter.Create(ms, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8, OmitXmlDeclaration = true });
                writer.WriteDocType("html", null, null, null);
                HtmlBuilder.OpenElement(writer, "html")
                    .OpenElement("head")
                        //.Element("meta", "charset", "utf-8")
                        .Element("meta", "name", "viewport", "content", "width=device-width, initial-scale=1.0")
                        .OpenElement("script", "src", "https://ajax.googleapis.com/ajax/libs/angularjs/1.5.5/angular.min.js").InnerText("").CloseElement()
                        .OpenElement("script", "src", "https://cdnjs.cloudflare.com/ajax/libs/angular-strap/v2.3.8/angular-strap.min.js").InnerText("").CloseElement()
                        .OpenElement("script", "src", "https://cdnjs.cloudflare.com/ajax/libs/angular-strap/v2.3.8/angular-strap.tpl.min.js").InnerText("").CloseElement()
                        .Element("link", "rel", "stylesheet", "href", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css")
                        .Element("link", "rel", "stylesheet", "href", "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css")
                        .OpenElement("title")
                            .InnerText(page.Title?? "Test")
                        .CloseElement()
                    .CloseElement()
                    .OpenElement("body")
                        .OpenElement("div", "data-ng-app", page.GetType().FullName, "data-ng-controller",page.ControllerObject.GetType().FullName)
                        .OpenElement("div", "class", "container")
                            .Build((w) =>
                                        {
                                            foreach (var element in page.Elements)
                                            {
                                                ConvertElement(w, element);
                                            }
                                        })
                        .CloseElement()
                        .CloseElement()
                        .OpenElement("script")
                        .WriteScript(CreateController(page, page.ModelObject, page.ControllerObject))
                        .InnerText("\r\n")
                        .CloseElement()
                    .CloseElement()
                .CloseElement()
                .Complete();


                string result = Encoding.UTF8.GetString(ms.ToArray()).Replace("<![CDATA[", "//<![CDATA[").Replace("]]","//]]");
                return result;
            }
        }


        private string CreateController(object page,  object model, object controller)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendFormat("var app = angular.module('{0}', []);", page.GetType().FullName);
            sb.AppendLine();
            sb.AppendFormat("app.controller('{0}', function($scope, $http) {{", controller.GetType().FullName);
            sb.AppendLine();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(model.GetType());
            string jsonString;
            using (MemoryStream ms = new MemoryStream())
            {
                ser.WriteObject(ms, model);
                jsonString = Encoding.UTF8.GetString(ms.ToArray());
                sb.AppendFormat("$scope.Model={0};", jsonString);
                sb.AppendLine();
            }
            foreach(var fn in controllerCalls.Values)
            {
                sb.AppendLine(fn);
            }
            sb.AppendLine("});");
            sb.AppendLine();
            return sb.ToString();
        }



        private void ConvertElement(XmlWriter writer, ViewElement element)
        {
            if (element is Container)
            {
                ConvertContainer(writer, (Container)element);
            }
            else if (element is ModelRefreshButtonBase)
            {
                ConvertModelRefreshButtonBase(writer, (ModelRefreshButtonBase)element);
            }
            else if (element is Button)
            {
                ConvertButton(writer, (Button)element);
            }
            else if (element is Label)
            {
                ConvertLabel(writer, (Label)element);
            }
            else if (element is NumberInput)
            {
                ConvertNumberInput(writer, (NumberInput)element);
            }
            else if (element is TextInput)
            {
                ConvertTextInput(writer, (TextInput)element);
            }
            else if (element is TableLayoutBase)
            {
                ConvertTableLayout(writer, (TableLayoutBase)element);
            }
        }

        private void ConvertContainer(XmlWriter writer, Container container)
        {
            HtmlBuilder.OpenElement(writer, "div", "class", "form-group")
                    .Build((w) =>
                    {
                        if (container.Elements.Count > 0)
                        {
                            foreach (var element in container.Elements)
                            {
                                ConvertElement(w, element);
                            }
                        }
                        else
                        {
                            w.WriteElementString("span", " ");
                        }
                    })
                .CloseElement()
                .Complete();
        }

        private void ConvertButton(XmlWriter writer, Button button)
        {
            HtmlBuilder.OpenElement(writer, "button", "class", "btn btn-default")
                .Build ((w)=> {
                        w.WriteString(GetExpressionBinding(button, true));
                    })
                .CloseElement()
                .Complete();
        }

        private void ConvertModelRefreshButtonBase(XmlWriter writer, ModelRefreshButtonBase button)
        {
            string id = "modelRefresh" + this.controllerCalls.Keys.Count.ToString();
            string url = ControllerRegistry.GetControllerUrl(button.OnClickMethod);
            string fn = string.Format("$scope.{0} = function() {{ \r\n$http.post('{1}',$scope.Model).then(function(response) \r\n{{\r\n$scope.Model = response.data; \r\n}});\r\n }};", id, url);
            controllerCalls.Add(id, fn);
            HtmlBuilder.OpenElement(writer, "button", "class", "btn btn-default", "ng-click", id+"()")
                .Build((w) => {
                    w.WriteString(GetExpressionBinding(button,true));
                })
                .CloseElement()
                .Complete();
        }
        private void ConvertLabel(XmlWriter writer, Label label)
        {
            HtmlBuilder.OpenElement(writer, "span")
                .Build((w) =>
                {
                    WriteExpressionBindingOrConstant(label, w);
                })
                .CloseElement()
                .Complete();
        }

        private void WriteExpressionBindingOrConstant(ViewElement element, XmlWriter w)
        {
            w.WriteString(GetExpressionBinding(element,true));
        }


        private string GetExpressionBinding(ViewElement element, bool isBindingExpression)
        {

            if (element.Value != null)
            {
                return element.Value.ToString();
            }
            else if (ExpressionHelper.IsValueAStringConstant(element.Expression))
            {
                return ExpressionHelper.GetStringConstant(element.Expression);
            }
            else if (ExpressionHelper.IsValueAModelRefererence(element.Expression))
            {
                var expressionString = ExpressionHelper.GetMemberReference(element.Expression);
                if (expressionString.Contains("~"))
                {
                    var subexpressions = expressionString.Split('~');
                    expressionString = "";
                    for (int i = 0; i < subexpressions.Length - 1; i++)
                    {
                        var repeter = repeaters.Find((t) => t.Item1 == subexpressions[i].Substring(0, subexpressions[i].Length - 1));
                        expressionString = expressionString + repeter.Item2;
                    }
                    expressionString += subexpressions[subexpressions.Length - 1];
                }
                return isBindingExpression ? "{{" + expressionString + "}}": expressionString;
            }
            return " ";
        }

        private void ConvertNumberInput(XmlWriter writer, NumberInput label)
        {
            HtmlBuilder.OpenElement(writer, "span")
                .OpenElement("input", "type", "text", "class", "form-control", "ng-model", GetExpressionBinding(label,false))
                .CloseElement()
                .CloseElement()
                .Complete();
        }

        private void ConvertTextInput(XmlWriter writer, TextInput label)
        {
            HtmlBuilder.OpenElement(writer, "span")
                .OpenElement("input", "type", "text", "class", "form-control", "ng-model", GetExpressionBinding(label,false))
                .CloseElement()
                .CloseElement()
                .Complete();
        }


        private void ConvertTableLayout(XmlWriter writer, TableLayoutBase table)
        {
            HtmlBuilder.OpenElement(writer, "div", "class", "container", "style","word-wrap: break-word;")
                .Build((w) => {
                    foreach (var row in table.Rows)
                    {
                        int numberOfUnassignedColumns = 0;
                        int numberOfAssignedColumns = 0;
                        int[] widths = row.Cells.Select(c => c.Width).ToArray();
                        foreach (var width in widths)
                        {
                            if (width == -1)
                            {
                                numberOfUnassignedColumns++;
                            }
                            else
                            {
                                numberOfAssignedColumns += width;
                            }
                        }
                        if (numberOfAssignedColumns + numberOfUnassignedColumns > 12)
                        {
                            throw new InvalidDataException("Invalid cell with count");
                        }
                        int columnWidth = 0;
                        if (numberOfUnassignedColumns > 0)
                        {
                            columnWidth = (12 - numberOfAssignedColumns) / numberOfUnassignedColumns;
                        }
                        int leftWith = 12;
                        for(int i =0; i < widths.Length; i++)
                        {
                            if (widths[i] == -1)
                            {
                                widths[i] = columnWidth;
                            }
                            leftWith -= widths[i];
                        }
                        if (leftWith != 0)
                        {
                            widths[widths.Length - 1] += leftWith;
                        }
                        if (row is RowLayoutRepeaterBase)
                        {
                            var repeaterRow = row as RowLayoutRepeaterBase;
                            var repeater = ExpressionHelper.GetMemberReference(repeaterRow.Expression);
                            var repVariable = "r" + repeaters.Count.ToString();
                            var newCurrent = new Tuple<string, string>(repeater, repVariable);
                            repeaters.Add(newCurrent);
                            string ngRepeatExpression = string.Format("{0} in {1}", repVariable, repeater);
                            var builder = HtmlBuilder.OpenElement(writer, "div", "class", "row", "ng-repeat", ngRepeatExpression);
                            for (int i = 0; i < widths.Length; i++)
                            {
                                builder.Build((ww) => { ConvertTableLayoutCell(ww, row.Cells[i], widths[i]); });
                            }
                            builder.CloseElement();
                            repeaters.Remove(newCurrent);
                        }
                        else
                        {
                            var builder = HtmlBuilder.OpenElement(writer, "div", "class", "row");
                            for (int i = 0; i < widths.Length; i++)
                            {
                                builder.Build((ww) => { ConvertTableLayoutCell(ww, row.Cells[i], widths[i]); });
                            }
                            builder.CloseElement();
                        }
                    }
                })
                .CloseElement()
                .Complete();
        }



        private void ConvertTableLayoutCell(XmlWriter writer, TableLayoutCell cell, int width)
        {
            HtmlBuilder.OpenElement(writer, "div", "class", string.Format("col-xs-{0}", width))
                .Build((w) => {
                    ConvertContainer(writer, cell);
                })
                .CloseElement()
                .Complete();
        }
    }
}
