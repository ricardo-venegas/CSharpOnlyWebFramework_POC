using PureCSharpWeb.Controllers;
using PureCSharpWeb.Framework;
using PureCSharpWeb.Framework.Elements;
using PureCSharpWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PureCSharpWeb.Views
{
    public class OrderEntrySample: Page<Order, OrderController, string>
    {
        public OrderEntrySample(Order order, OrderController controller): base (order, controller)
        {
            this.Elements = new List<ViewElement> {
                new Container { Elements = { new EntitySelector(), new Label("Name:"), new Label(()=>Model.Customer.Name)}  } ,
                new Container {
                    Elements = {
                        new TableLayout()
                        {
                            Rows = {
                                new TableLayoutRow() {
                                    Cells =
                                    {
                                      new TableLayoutCell() { Elements = { new Label("Product")}, Width =5 },
                                      new TableLayoutCell() { Elements = { new Label("Quantity")},Width =2 },
                                      new TableLayoutCell() { Elements = { new Label("Price")},Width =2 },
                                      new TableLayoutCell() { Elements = { new Label("Extended Price")},Width =2 },
                                      new TableLayoutCell() { Width =1 },
                                    }

                                },
                                new TableLayoutRowRepeater<LineItem>(()=>Model.LineItems) {
                                    Cells =
                                    {
                                      new TableLayoutCell() { Elements = { new Label(() => Model.LineItems.Current().ProductId)  }, Width =5  },
                                      new TableLayoutCell() { Elements = { new Label(() => Model.LineItems.Current().Quantity) }, Width =2 },
                                      new TableLayoutCell() { Elements = { new Label(()=> Model.LineItems.Current().Price) }, Width =2  },
                                      new TableLayoutCell() { Elements = { new Label(()=> Model.LineItems.Current().ExtendedPrice) }, Width =2 },
                                      new TableLayoutCell() { Elements = { new Button("Delete") },Width =1 },
                                    },
                                },
                                new TableLayoutRow() {
                                    Cells =
                                    {
                                      new TableLayoutCell() { Width =5 },
                                      new TableLayoutCell() { Width =2 },
                                      new TableLayoutCell() { Width =2 },
                                      new TableLayoutCell() { Elements = { new Label(()=>Model.SubTotal) },Width =2 },
                                      new TableLayoutCell() { Width =1 },
                                    }

                                },
                                new TableLayoutRow() {
                                    Cells =
                                    {
                                      new TableLayoutCell() { Elements = { new TextInput(()=>Model.NewLineItem.ProductId) },  Width =5 },
                                      new TableLayoutCell() { Elements = { new TextInput(()=>Model.NewLineItem.Quantity) }, Width =2 },
                                      new TableLayoutCell() { Elements = { new TextInput(()=>Model.NewLineItem.Price) },Width =2 },
                                      new TableLayoutCell() { Elements = { new TextInput(()=>Model.NewLineItem.ExtendedPrice) },Width =2 },
                                      new TableLayoutCell() { Elements = { new ModelRefreshButton<Order>(() => "Add") { OnClick = OrderController.AddItem } },Width =1 }
                                    }

                                }
                            }
                        }
                    }
                }
                };
        }
    }
}
