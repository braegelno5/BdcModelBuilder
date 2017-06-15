// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebberCross.BdcModelBuilder.BdcModelTemplate
{
    public class Utilities
    {
        // Common exception handler for all models
        public void HandleException(Exception ex)
        {
            // Exception should be handled here
            // If it is rethrown, it will be cause the consuming page to error
            throw ex;
        }
    }
}
