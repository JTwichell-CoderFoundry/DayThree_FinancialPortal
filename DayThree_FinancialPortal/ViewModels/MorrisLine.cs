﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayThree_FinancialPortal.ViewModels
{
    public class MorrisLine
    {
        public string Year { get; set; }
        public int Value { get; set; }
    }

    public class MorrisDonut
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }

    public class MorrisBudgetBar
    {
        public string Label { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }
        public string  BarColor { get; set; }

    }

    public class MorrisBudgetItemBar
    {
        public string Label { get; set; }      
        public decimal Actual { get; set; }
    }

}