using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;

namespace DLL
{
    public class GraphicsClass
    {

        Bitmap bmp;
        Graphics g;
        int bmpX;
        int bmpY;
        float bmpB; //空白边占比（0~1）

        public GraphicsClass(int X, int Y, float Border)
        {
            bmp = new Bitmap(X, Y);
            bmpX = X;
            bmpY = Y;
            bmpB = Border;
            g = Graphics.FromImage(bmp);
        }


        public Bitmap DrawingIMG()
        {

            Matrix m = MartixPoint();
            

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.Bilinear;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;
            //清空画布，并以白色背景填充
            g.Clear(System.Drawing.Color.White);




 

            //用这个画笔在左上角画一个矩形
            //g.DrawRectangle(pen, new Rectangle(0, 0, 200, 300));


            //画多边形
            if (dtPolygon != null)
            {
                
                foreach (DataRow item in dtPolygon.Rows)
                {
                    m.TransformPoints((PointF[])item["Point"]);

                    //定义一枝画笔
                    Pen pen = new Pen((Color)item["Color"]);
                    g.DrawPolygon(pen, (PointF[])item["Point"]);
                }
            }


            //画点、圆、椭圆
            if (dtEllipse != null)
            {
                
                foreach (DataRow item in dtEllipse.Rows)
                {
                    m.TransformPoints((PointF[])item["Point"]);

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Brush bush = new SolidBrush((Color)item["Color"]);//填充的颜色

                    int owight = Convert.ToInt32(item["Wight"]);
                    int ohigh = Convert.ToInt32(item["High"]);
                    foreach (PointF item1 in (PointF[])item["Point"])
                    {
                        g.FillEllipse(bush, item1.X, item1.Y, owight, ohigh);
                    }


                }
            }


            //画线
            if (dtLine != null)
            {

                foreach (DataRow item in dtLine.Rows)
                {
                    m.TransformPoints((PointF[])item["Point"]);

                    //定义一枝画笔
                    Pen pen = new Pen((Color)item["Color"]);

                    g.DrawLines(pen, (PointF[])item["Point"]);

                }
            }


            //画文字
            if (dtText != null)
            {
                foreach (DataRow item in dtText.Rows)
                {
                    PointF[] pp = {(PointF)item["Point"] };
                    m.TransformPoints(pp);

                    GraphicsText graphicsText = new GraphicsText();

                    graphicsText.Graphics = g;

                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    graphicsText.DrawString(item["Text"].ToString(), new Font("宋体", Convert.ToInt16(item["Size"])), new SolidBrush((Color)item["Color"]), pp[0], format, (float)Convert.ToDecimal(item["Angle"]));
                }


            }


            return bmp;
        }



        //获取最大最小值
        bool InitXY = true;
        float MaxX;
        float MinX;
        float MaxY;
        float MinY;
        void GetLimit(params PointF[] oPoint)
        {
            foreach (PointF item in oPoint)
            {
                if (InitXY)
                {
                    MaxX = item.X;
                    MinX = MaxX;
                    MaxY = item.Y;
                    MinY = MaxY;
                    InitXY = false;
                    continue;
                }

                if (item.X > MaxX)
                {
                    MaxX = item.X;
                }

                if (item.X < MinX)
                {
                    MinX = item.X;
                }

                if (item.Y > MaxY)
                {
                    MaxY = item.Y;
                }

                if (item.Y < MinY)
                {
                    MinY = item.Y;
                }
            }
        }

        //根据最大最小值计算矩阵
        Matrix MartixPoint()
        {
            float Xmove = 0 - MinX;
            float Ymove = 0 - MinY;

            float XScale = bmpX/(MaxX - MinX);
            float YScale = bmpY/(MaxY - MinY);

            float ZScale = XScale;
            if (YScale < XScale)
	        {
		        ZScale = YScale;
	        }

            Matrix m = new Matrix();


            m.Translate(Xmove, Ymove, MatrixOrder.Append);
            m.Scale(ZScale, ZScale, MatrixOrder.Append);





            //边界
            m.Scale((1 - bmpB), (1 - bmpB), MatrixOrder.Append);
            m.Translate((bmpX * bmpB) / 2, (bmpY * bmpB) / 2, MatrixOrder.Append);

            //翻转
            m.Scale(1, -1, MatrixOrder.Append);
            m.Translate(0, bmpY, MatrixOrder.Append);


            //中心平移

            //m.Translate(bmpX / 2 - (MaxX * ZScale - MinX * ZScale) / 2, bmpY / 2 - (MaxY * ZScale - MinY * ZScale) / 2, MatrixOrder.Append);


            return m;
        }



        public void SaveJpg(string Path, bool Open)
        {
            if (Path != "")
            {
                bmp.Save(Path, System.Drawing.Imaging.ImageFormat.Jpeg);
            }



            if (Open)
            {
                System.Diagnostics.Process.Start(Path);
            }

        }




        DataTable dtEllipse = null;
        public void AddEllipse(PointF[] oPoint,Color oColor,int oWight,int oHigh)
        {
            GetLimit(oPoint);

            if (dtEllipse == null)
            {
                dtEllipse = new DataTable();
                dtEllipse.Columns.Add("Point", typeof(PointF[]));
                dtEllipse.Columns.Add("Color", typeof(Color));
                dtEllipse.Columns.Add("Wight", typeof(int));
                dtEllipse.Columns.Add("High", typeof(int));
            }


            DataTableClass.DtAddRow(dtEllipse, oPoint, oColor, oWight, oHigh);


        }



        DataTable dtPolygon = null;
        public void AddPolygon(PointF[] oPoint, Color oColor)
        {
            GetLimit(oPoint);

            if (dtPolygon == null)
            {
                dtPolygon =  new DataTable();
                dtPolygon.Columns.Add("Point", typeof(PointF[]));
                dtPolygon.Columns.Add("Color", typeof(Color));
            }


            DataTableClass.DtAddRow(dtPolygon, oPoint, oColor);

        }


        DataTable dtText = null;
        public void AddText(PointF oPoint, string oText ,Color oColor,int oSize ,float oAngle)
        {
            GetLimit(oPoint);

            if (dtText == null)
            {
                dtText = new DataTable();
                dtText.Columns.Add("Point", typeof(PointF));
                dtText.Columns.Add("Text");
                dtText.Columns.Add("Color", typeof(Color));
                dtText.Columns.Add("Size",typeof(int));
                dtText.Columns.Add("Angle", typeof(int));
            }


            DataTableClass.DtAddRow(dtText, oPoint, oText, oColor, oSize, oAngle);
        }


        public DataTable dtLine = null;
        public void AddLine(PointF[] oPoint, Color oColor,string Name)
        {
            GetLimit(oPoint);

            if (dtLine == null)
            {
                dtLine = new DataTable();
                dtLine.Columns.Add("Point", typeof(PointF[]));
                dtLine.Columns.Add("Color", typeof(Color));
                dtLine.Columns.Add("Name", typeof(string));
            }


            DataTableClass.DtAddRow(dtLine, oPoint, oColor, Name);
        }




        public Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //  对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            //  为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

            return System.Drawing.Color.FromArgb(int_Red, int_Green, int_Blue);
        }




    }


    class GraphicsText
    {
        private Graphics _graphics;

        public GraphicsText()
        {

        }

        public Graphics Graphics
        {
            get { return _graphics; }
            set { _graphics = value; }
        }

        /// <summary>  
        /// 绘制根据矩形旋转文本  
        /// </summary>  
        /// <param name="s">文本</param>  
        /// <param name="font">字体</param>  
        /// <param name="brush">填充</param>  
        /// <param name="layoutRectangle">局部矩形</param>  
        /// <param name="format">布局方式</param>  
        /// <param name="angle">角度</param>  
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format, float angle)
        {
            // 求取字符串大小  
            SizeF size = _graphics.MeasureString(s, font);

            // 根据旋转角度，求取旋转后字符串大小  
            SizeF sizeRotate = ConvertSize(size, angle);

            // 根据旋转后尺寸、布局矩形、布局方式计算文本旋转点  
            PointF rotatePt = GetRotatePoint(sizeRotate, layoutRectangle, format);

            // 重设布局方式都为Center  
            StringFormat newFormat = new StringFormat(format);
            newFormat.Alignment = StringAlignment.Center;
            newFormat.LineAlignment = StringAlignment.Center;

            // 绘制旋转后文本  
            DrawString(s, font, brush, rotatePt, newFormat, angle);
        }

        /// <summary>  
        /// 绘制根据点旋转文本，一般旋转点给定位文本包围盒中心点  
        /// </summary>  
        /// <param name="s">文本</param>  
        /// <param name="font">字体</param>  
        /// <param name="brush">填充</param>  
        /// <param name="point">旋转点</param>  
        /// <param name="format">布局方式</param>  
        /// <param name="angle">角度</param>  
        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format, float angle)
        {
            // Save the matrix  
            Matrix mtxSave = _graphics.Transform;

            Matrix mtxRotate = _graphics.Transform;
            mtxRotate.RotateAt(angle, point);
            _graphics.Transform = mtxRotate;

            _graphics.DrawString(s, font, brush, point, format);

            // Reset the matrix  
            _graphics.Transform = mtxSave;
        }

        private SizeF ConvertSize(SizeF size, float angle)
        {
            Matrix matrix = new Matrix();
            matrix.Rotate(angle);

            // 旋转矩形四个顶点  
            PointF[] pts = new PointF[4];
            pts[0].X = -size.Width / 2f;
            pts[0].Y = -size.Height / 2f;
            pts[1].X = -size.Width / 2f;
            pts[1].Y = size.Height / 2f;
            pts[2].X = size.Width / 2f;
            pts[2].Y = size.Height / 2f;
            pts[3].X = size.Width / 2f;
            pts[3].Y = -size.Height / 2f;
            matrix.TransformPoints(pts);

            // 求取四个顶点的包围盒  
            float left = float.MaxValue;
            float right = float.MinValue;
            float top = float.MaxValue;
            float bottom = float.MinValue;

            foreach (PointF pt in pts)
            {
                // 求取并集  
                if (pt.X < left)
                    left = pt.X;
                if (pt.X > right)
                    right = pt.X;
                if (pt.Y < top)
                    top = pt.Y;
                if (pt.Y > bottom)
                    bottom = pt.Y;
            }

            SizeF result = new SizeF(right - left, bottom - top);
            return result;
        }

        private PointF GetRotatePoint(SizeF size, RectangleF layoutRectangle, StringFormat format)
        {
            PointF pt = new PointF();

            switch (format.Alignment)
            {
                case StringAlignment.Near:
                    pt.X = layoutRectangle.Left + size.Width / 2f;
                    break;
                case StringAlignment.Center:
                    pt.X = (layoutRectangle.Left + layoutRectangle.Right) / 2f;
                    break;
                case StringAlignment.Far:
                    pt.X = layoutRectangle.Right - size.Width / 2f;
                    break;
                default:
                    break;
            }

            switch (format.LineAlignment)
            {
                case StringAlignment.Near:
                    pt.Y = layoutRectangle.Top + size.Height / 2f;
                    break;
                case StringAlignment.Center:
                    pt.Y = (layoutRectangle.Top + layoutRectangle.Bottom) / 2f;
                    break;
                case StringAlignment.Far:
                    pt.Y = layoutRectangle.Bottom - size.Height / 2f;
                    break;
                default:
                    break;
            }

            return pt;
        }
    }  

}
