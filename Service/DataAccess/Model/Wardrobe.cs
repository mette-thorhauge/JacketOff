﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess {
    public class Wardrobe {

        public string ID { get; set; }

        public int MaxAmountOfItems { get; set; }

        public int Count { get; set; }

        public byte[] RowID { get; set; }

        public Int64 RowIDBig { get; set; }


    }
}
