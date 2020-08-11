using Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface INumService
    {
        NumModel Get(string numType);
    }
    public class NumService : INumService
    {
        private int min = 2;
        private int max = 100;
        public NumModel Get(string numType)
        {
            Random random = new Random();
            NumModel model = new NumModel()
            {
                NumType = numType,
                NumSeries = new List<CustomChartItem>()
            };
            for (int i = 0; i < 10; i++)
            {
                int nextnum = random.Next(min / 2, max / 2) * 2 + (numType == "odd" ? 1 : 0);
                model.NumSeries.Add(new CustomChartItem() { XVal = i, YVal = nextnum });
            }
            return model;
        }
    }
}
