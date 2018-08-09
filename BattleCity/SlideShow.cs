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

        public Slide GetSlide(float elapsedSeconds = 0)
        {
            Slide curSlide = _slideList[0];
            if (elapsedSeconds > 0 && _slideList.Count != 1)
            {
                float summedDisplayTime = 0;
                foreach (Slide slide in _slideList)
                {
                    curSlide = slide;
                    summedDisplayTime += slide.GetDisplayTime();
                    if (summedDisplayTime > elapsedSeconds)
                        break;
                }
            }
            return curSlide;
        }

        public float GetTotalDisplayTime()
        {
            float summedDisplayTime = 0;
            foreach (Slide slide in _slideList)
            {
                summedDisplayTime += slide.GetDisplayTime();
            }
            return summedDisplayTime;
        }

    }
}
