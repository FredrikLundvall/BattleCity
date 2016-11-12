using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    public class SlideShow
    {
        IList<Slide> _slideList; 
        
        public SlideShow(IList<Slide> slideList)
        {
            _slideList = slideList;
        }

        public void AddSlide(Slide slide)
        {
            _slideList.Add(slide);
        }

        public Slide GetSlide()
        {
            return _slideList[0];//Det blir mer senare
        }
    }
}
