using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Results
{
    public class CommonResult<T>
    {
        T Result { get; set; }

        public CommonResult(T result)
        {
            this.Result = result;
        }
        
    }
}
