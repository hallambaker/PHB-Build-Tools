using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goedel.Command {
    /// <summary>Track start and end time of parse.</summary>
    public abstract class Dispatch {
        /// <summary>Record start time.</summary>
        public DateTime Started = DateTime.Now;

        /// <summary>Calculate elapsed time.</summary>
        public TimeSpan Elapsed { get => DateTime.Now - Started; }

        /// <summary></summary>
        public virtual Goedel.Command.Type[] _Data { get; set; }

        public virtual DescribeCommandEntry DescribeCommand { get; set; }
        }

    }
