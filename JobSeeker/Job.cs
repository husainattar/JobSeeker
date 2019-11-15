using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker
{
    class Job
    {
        public string title, location, url;
        public Job(string title) {
            this.title = title;    
        }
         
        public override string ToString(){
            return title + location;
        }

    }
}
