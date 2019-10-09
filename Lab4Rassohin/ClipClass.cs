using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Rassohin
{

    public class ClipClass
    {
        public float x0, y0, x1, y1;

        private float _xLeft, _xRight, _yBottom, _yTop; //размеры окна

        public float _xN, _yN; //начало отрезка
        public float _xK, _yK; //конец отрезка
     
        float dx, dy; // приращения координат
        float dxdy, dydx; //наклон отрезка к сторонам

        int cN, cK; //коды начала и конца отрезка
        int visible; //0 или 1 - не видим/видим

        public ClipClass(float xLeft, float xRight, float yBottom, float yTop, float x0, float y0, float x1, float y1)
        {
            _xLeft = xLeft;
            _xRight = xRight;
            _yBottom = yBottom;
            _yTop = yTop;
            _xN = x0;
            _yN = y0;
            _xK = x1;
            _yK = y1;
        }

        public ClipClass()
        {
            _xLeft = 0;
            _xRight = 8;
            _yBottom = 0;
            _yTop = 4;
            _xN = 8;
            _yN = 4;
            _xK = -4;
            _yK = -4;
        }




        /* вычисление кода точки */
        public int code(float x, float y)
        {

            if (x < _xLeft)
            {
                return 1;
            }
            if (x > _xRight)
            {
                return 2;
            }
            if (y < _yBottom)
            {
                return 4;
            }
            if (y > _yTop)
            {
                return 8;
            }
            return 0;
        }

       public void clip()
        {
            //вычисляем коды
          

            cN = code(_xN, _yN);
            cK = code(_xK, _yK);
            //определяем приращения
            dx = _xK - _xN; 
            dy = _yK - _yN;
            //если приращение не 0, то вычисляем наклон, если оба приращения 0, то проверяем точки на коды, если они нули, то отрезок внутри
            if (dx != 0)
            {
                dydx = dy / dx;
            }
            else
            {
                if (dy == 0)
                {
                    if (cN == 0 && cK == 0) goto Out;
                }
            }
            if (dy != 0) dxdy = dx / dy;

            visible = 0; int ii = 4;

            do
            {
                if (cN == cK) break; //целиком вне окна
                if (cN == 0 && cK == 0) //целиком внутри
                {
                    ++visible;
                    break;
                }
                if(cN == 0)
                {
                    int s = cN; cN = cK; cK = s; //меняем местами
                    float r = _xN; _xN = _xK; _xK = r;
                    r = _yN; _yN = _yK; _yK = r;
                }

                //делим отрезок, помещая точку Pn в точку пересечения отрезка со стороной окна
                if (cN == 1)
                {         /* Пересечение с левой стороной */
                    _yN = _yN + dydx * (_xLeft - _xN);
                    _xN = _xLeft;
                }
                else if (cN == 2)
                {  /* Пересечение с правой стороной*/
                    _yN = _yN + dydx * (_xRight - _xN);
                    _xN = _xRight;
                }
                else if (cN == 4)
                {  /* Пересечение в нижней стороной*/
                    _xN = _xN + dxdy * (_yBottom - _yN);
                    _yN = _yBottom;
                }
                else if (cN == 8)
                {  /*Пересечение с верхней стороной*/
                    _xN = _xN + dxdy * (_yTop - _yN);
                    _yN = _yTop;
                }

                cN = code(_xN, _yN);
               
            }
            while (--ii >= 0);

            Out: { x0 = _xN; y0 = _yN; x1 = _xK; y1 = _yK; }
         

        }



    }


}