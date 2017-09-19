using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Collections;
using System.Reflection;

namespace JM.Helpers
{
   
    public static class ModelStateHelper
    {

        /// <summary>
        /// Return Error Messages
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static List<String> ReturnErrorMessages(IEnumerable errors)
        {

            var r = new System.Text.RegularExpressions.Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);

            List<String> errorMessages = new List<String>();

            foreach (KeyValuePair<string, string[]> item in errors)
            {
                string errorMessage;
                
                string keyValue = item.Key;

                string[] objectProperty = keyValue.Split(Convert.ToChar("."));
                if (objectProperty.Length == 0)
                    errorMessage = item.Value[0];
                else
                {
                    errorMessage = objectProperty[1] + ": " + item.Value[0];
                    if (errorMessage.Contains(objectProperty[1]))
                    {
                        errorMessage = errorMessage.Replace(objectProperty[1], "");
                        errorMessage = objectProperty[1] + errorMessage;
                        errorMessage = errorMessage.Replace(" for ", "");                     
                    }
                }

                errorMessage = r.Replace(errorMessage, " ");
                errorMessage = errorMessage.Replace(" .", ".");
                errorMessages.Add(errorMessage);
            }
            return errorMessages;
        }
        /// <summary>
        /// Return Validation Errors
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static Hashtable ReturnValidationErrors(IEnumerable errors)
        {
            Hashtable validationErrors = new Hashtable();

            foreach (KeyValuePair<string, string[]> item in errors)
            {
                string keyValue = item.Key;
                string errorMessage = item.Value[0];
                string[] objectProperty = keyValue.Split(Convert.ToChar("."));
                if (objectProperty.Length > 1)
                {
                    if (validationErrors.ContainsKey(objectProperty[1]) == false)
                        validationErrors.Add(objectProperty[1], errorMessage);
                }
            }
            return validationErrors;
        }
        /// <summary>
        /// Errors
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage).ToArray())
                                    .Where(m => m.Value.Count() > 0);
            }
            return null;
        }

        /// <summary>
        /// Update View Model
        /// </summary>
        /// <param name="dataTransformationObject"></param>
        /// <param name="businessObject"></param>
        public static void UpdateViewModel(object dataTransformationObject, object businessObject)
        {
            Type targetType = businessObject.GetType();
            Type sourceType = dataTransformationObject.GetType();

            PropertyInfo[] sourceProps = sourceType.GetProperties();
            foreach (var propInfo in sourceProps)
            {              
                //Get the matching property from the target
                PropertyInfo toProp =  (targetType == sourceType) ? propInfo : targetType.GetProperty(propInfo.Name);

                //If it exists and it's writeable
                if (toProp != null && toProp.CanWrite)
                {
                    //Copy the value from the source to the target
                    Object value = propInfo.GetValue(dataTransformationObject, null);
                    toProp.SetValue(businessObject, value, null);
                }
            }


        }

    }

}