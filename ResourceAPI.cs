//----------------------------------------------------------------
// ResourceAPI.cs
// Copyright SimplyCast 2014
// This projected is licensed under the terms of the MIT license.
//  (see the attached LICENSE.txt).
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimplyCast
{
    /// <summary>
    /// This class is the one that all resource collection APIs decend from.
    /// </summary>
    abstract public class ResourceAPI
    {
        /// <summary>
        /// The API connection used to make requests with.
        /// </summary>
        protected SimplyCastAPIConnector connection;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connection">The API connection, provided by 
        /// the SimplyCastAPI class.</param>
        public ResourceAPI(SimplyCastAPIConnector connection)
        {
            this.connection = connection;
        }
    }
}
