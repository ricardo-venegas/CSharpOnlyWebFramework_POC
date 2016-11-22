using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PureCSharpWeb.Framework
{
    public static class ControllerRegistry
    {
        static Dictionary<string, MethodInfo> controllers = new Dictionary<string, MethodInfo>();
        static Dictionary<string, string> controllerUrl = new Dictionary<string, string>();
        public static string GetControllerUrl<M>(Func<M,M> controllerFunction)
        {
            return GetControllerUrl(controllerFunction.GetMethodInfo());
        }


        public static string GetControllerUrl(MethodInfo controllerFunction)
        {
            string methodId = controllerFunction.ToString().Replace(" ", "::");
            if (controllerUrl.Keys.Contains(methodId) == false)
            {
                controllerUrl[methodId] = "/" + controllerFunction.DeclaringType.Name + "/" + controllerFunction.Name;
                controllers[methodId] = controllerFunction;
            }
            return controllerUrl[methodId];
        }

        public static string ExecuteController(string url, Stream content)
        {
            var methodId = controllerUrl.FirstOrDefault(kp => kp.Value == url).Key;
            var methodInfo = controllers[methodId];
            var requetType = methodInfo.GetParameters()[0].ParameterType;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(requetType);
            object request = ser.ReadObject(content);
            object response = methodInfo.Invoke(null, new object[] { request });
            string jsonString;
            using (MemoryStream ms = new MemoryStream())
            {
                ser.WriteObject(ms, response);
                jsonString = Encoding.UTF8.GetString(ms.ToArray());
                return jsonString;
            }
        }
    }
}
