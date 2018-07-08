using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Tcp4
{
   public class NavData : INotifyPropertyChanged
    {
       public event PropertyChangedEventHandler PropertyChanged;
       private int reflectors;
       private double x;
       private double y;
       private double angle;

       public int Reflectors
       {
           get { return reflectors; }
           set
           {
               reflectors = value;
               if (this.PropertyChanged != null)
               {
                   this.PropertyChanged(this, new PropertyChangedEventArgs("Reflectors"));
               }
           }
       }

       public double X
       {
           get { return x; }
           set
           {
               x = value;
               if (this.PropertyChanged != null)
               {
                   this.PropertyChanged(this, new PropertyChangedEventArgs("X"));
               }
           }
       }

       public double Y
       {
           get { return y; }
           set
           {
               y = value;
               if (this.PropertyChanged != null)
               {
                   this.PropertyChanged(this, new PropertyChangedEventArgs("Y"));
               }
           }
       }

       public double Angle
       {
           get { return angle; }
           set
           {
               angle = value;
               if (this.PropertyChanged != null)
               {
                   this.PropertyChanged(this, new PropertyChangedEventArgs("Angle"));
               }
           }
       }
    }
}
