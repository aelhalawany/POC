using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace Poc.Core.ActionResult
{
    public class ActionResult
    {
        public bool Result { get; set; } = false;

        public List<string> ValidationErrorKeys { get; set; } = new List<string>();
        public ActionResult()
        {
            //if(this.ValidationErrorKeys==null)
            //    this.ValidationErrorKeys = new List<string>();
        }
        public ActionResult(bool success)
        {
            this.Result = success;
        }

        public static ActionResult Success => new ActionResult(true);

        //public ActionResult ValidationFailed
        //{
        //    get
        //    {
        //        if (ValidationErrorKeys.Count > 0)
        //            return new ActionResult(false);
        //        else
        //            throw new ArgumentNullException("No Validation Found");
        //    }

        //}
        public List<string> ReplaceValidationKeys(string GlobalResourceName)
        {
            List<string> values = new List<string>();
            if(this.ValidationErrorKeys!=null)
            {
                foreach (var key in this.ValidationErrorKeys)
                {
                    values.Add(Convert.ToString(HttpContext.GetGlobalResourceObject(GlobalResourceName,key)));
                }
            }
            return values;
        }
    }
}
