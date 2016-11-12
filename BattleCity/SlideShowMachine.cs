using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    public class SlideShowMachine
    {
        public static readonly int SLIDESHOW_RIGHT = 1;

        IDictionary<int, SlideShow> _slideShowDictionary;

        public SlideShowMachine(IDictionary<int, SlideShow> slideShowDictionary)
        {
            _slideShowDictionary = slideShowDictionary;
        }

        public void AddSlideShow(int key, SlideShow slideShow)
        {
            _slideShowDictionary.Add(key, slideShow);
        }

        public SlideShow GetSlideShow(int key)
        {
            return _slideShowDictionary[key];
        }


    }
}
