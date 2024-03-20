using System.Numerics;

namespace Zajecia3
{
    public abstract class Container(int mass, int height, int tareweight, int volume, int maxload, String serialnumber)
    {
        public virtual void Empty()
        {
            _mass = 0;
        }
        public virtual void Load(int mass)
        {
            if (mass > _maxLoad) throw new OverfillException();
            _mass = mass;
        }
        private static int _containerIndex = 0;
        private int _id { get; } = _containerIndex++;
        protected int _mass { get; set; } = mass;
        protected int _height { get; } = height;
        protected int _tareWeight { get; } = tareweight;
        protected int _volume { get; } = volume;
        protected int _maxLoad { get; } = maxload;
        protected String _serialNumber { get; } = serialnumber;
    }
    public class LiquidContainer(int mass, int height, int tareweight, int volume, int maxload, String serialnumber)
    : Container(mass, height, tareweight, volume, maxload, serialnumber), IHazardNotifier
    {
        public void Load(int mass, bool isDangerous)
        {
            if (mass > (float)(_maxLoad * 9 / 10)) Notify();
            else if (isDangerous && mass > (float)(_maxLoad / 2)) Notify();
            _mass = mass;
            _isDangerous = isDangerous;
        }
        public void Notify()
        {
            System.Console.WriteLine("buhu, {} is dangerous", _serialNumber);
        }
        protected bool _isDangerous { get; set; }
    }
    public class GasContainer : Container,IHazardNotifier
    {
        public GasContainer(int mass, int height, int tareweight, int volume, int maxload, String serialnumber, int pressure)
        : base(mass, height, tareweight, volume, maxload, serialnumber)
        {
            _pressure = pressure;
        }
        public override void Empty()
        {
            _mass = (_mass/20);
        }
        private int _pressure;
    }
    public class CoolingContainer : Container
    {
        public CoolingContainer(int mass, int height, int tareweight, int volume, int maxload, String serialnumber)
        : base(mass, height, tareweight, volume, maxload, serialnumber)
        {

        }
    }
}