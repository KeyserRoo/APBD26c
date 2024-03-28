using System.Text;
namespace Zajecia3 {

	namespace Zajecia3 {
		public abstract class Container : IHazardNotifier {
			protected static int _containerIndex = 0;
			protected int _id { get; } = _containerIndex++;

			protected int _mass { get; set; }
			public int Mass { get { return _mass; } set { _mass = value; } }

			protected int _height { get; }
			protected int _tareWeight { get; }
			protected int _volume { get; }
			protected int _maxLoad { get; }

			protected string _serialNumber { get; }
			public string SerialNumber { get { return _serialNumber; } }

			protected Container(int height, int tareweight, int volume, int maxload, string serialnumber) {
				_height = height;
				_tareWeight = tareweight;
				_volume = volume;
				_maxLoad = maxload;
				_serialNumber = serialnumber;
			}

			public virtual void Empty() { _mass = 0; }

			public virtual void Load(int mass) {
				if (mass < 0) throw new LessThanZeroException("Nie ma takiej masy głupolu");
				if (mass > _maxLoad) throw new OverfillException("Container cannot support that weight");
				_mass = mass;
			}

			public virtual void Notify() { }

			public virtual string Describe() {
				return "Default description for Container";
			}
		}

		public class LiquidContainer : Container {
			private LiquidContainer(int height, int tareweight, int volume, int maxload)
			: base(height, tareweight, volume, maxload, $"KON-L-{_containerIndex}") { }
			public static LiquidContainer Create(int height, int tareweight, int volume, int maxload) {
				return new LiquidContainer(height, tareweight, volume, maxload);
			}
			public override string Describe() {
				return $"{GetType().Name}: Serial Number - {_serialNumber}, Mass - {_mass}, Is Dangerous - {_isDangerous}";
			}
			public override void Notify() { Console.WriteLine($"buhu, {_serialNumber} is dangerous"); }
			public void Load(int mass, bool isDangerous) {
				if (mass > (float)(_maxLoad * 9 / 10)) Notify();
				else if (isDangerous && mass > (float)(_maxLoad / 2)) Notify();
				_mass = mass;
				_isDangerous = isDangerous;
			}
			protected bool _isDangerous { get; set; } = false;
		}

		public class GasContainer : Container {
			private GasContainer(int height, int tareweight, int volume, int maxload)
			: base(height, tareweight, volume, maxload, "KON-L-" + _containerIndex) { }
			public static GasContainer Create(int height, int tareweight, int volume, int maxload) {
				return new GasContainer(height, tareweight, volume, maxload);
			}
			public override string Describe() {
				return $"{GetType().Name}: Serial Number - {_serialNumber}, Mass - {_mass}, Pressure - {_pressure}";
			}
			public override void Notify() { Console.WriteLine($"buhu, {_serialNumber} is dangerous"); }
			public override void Empty() {
				_mass = (int)(_mass * 0.05);
			}
			private int _pressure { get; set; } = 0;
		}

		public class CoolingContainer : Container {
			private CoolingContainer(int height, int tareweight, int volume, int maxload)
			: base(height, tareweight, volume, maxload, "KON-L-" + _containerIndex) { }
			public static CoolingContainer Create(int height, int tareweight, int volume, int maxload) {
				return new CoolingContainer(height, tareweight, volume, maxload);
			}
			public override string Describe() {
				return $"{GetType().Name}: Serial Number - {_serialNumber}, Mass - {_mass}, Product - {_product}, Temperature - {_temperature}";
			}
			private string _product { get; set; } = "";
			public string Procuct { get { return _product; } set { _product = value; } }
			private double _temperature { get; set; } = 0;
			private Dictionary<string, double> _prodTemp = new Dictionary<string, double>{
						{"Bananas",13.3},{"Chocolate",18},{"Fish",2},{"Meat",-15},{"Ice Cream",-18},
						{"Frozen pizza",-30},{"Cheese",7.2},{"Sausages",5},{"Butter",20.5},{"Eggs",19}
				};
		}

		public class Boat {
			private Boat(int maxSpeed, int maxcapacity, int maxLoadWeight) {
				_maxCapacity = maxcapacity;
				_maxLoadWeight = maxLoadWeight;
				_maxSpeed = maxSpeed;
			}
			public static Boat Create(int maxSpeed, int maxcapacity, int maxLoadWeight) {
				return new Boat(maxSpeed, maxcapacity, maxLoadWeight);
			}
			public void LoadBoat(Container container) {
				if (_capacity >= _maxCapacity) {
					throw new CapacityException("No space for more containers!");
				}
				if (_loadweight + container.Mass > _maxLoadWeight) {
					throw new OverfillException("Container is too heavy for current load");
				}
				_containers[container.SerialNumber] = container;
				_capacity++;
				_loadweight += container.Mass;
			}
			public void LoadBoat(List<Container> containers) {
				if (_capacity + containers.Count > _maxCapacity) throw new CapacityException("No space for more containers!");
				int sum = 0;
				foreach (Container item in containers) {
					sum += item.Mass;
				}
				if (_loadweight + sum > _maxLoadWeight) throw new OverfillException("Containers are too heavy for current load");
				foreach (var item in containers) {
					LoadBoat(item);
				}
			}
			public void DestroyContainer(string serialNumber) {
				if (_containers.ContainsKey(serialNumber)) {
					_capacity--;
					_loadweight -= _containers[serialNumber].Mass;
					_containers.Remove(serialNumber);
				}
				else {
					throw new ArgumentException("No container found with the specified serial number.");
				}
			}
			public Container DeLoadContainer(string serialNumber) {
				if (_containers.ContainsKey(serialNumber)) {
					Container container = _containers[serialNumber];
					_capacity--;
					_loadweight -= container.Mass;
					_containers.Remove(serialNumber);
					return container;
				}
				else {
					throw new ArgumentException("No container found with the specified serial number.");
				}
			}
			public void ReplaceContainer(string oldSerialNumber, Container newContainer) {
				if (_containers.ContainsKey(oldSerialNumber)) {
					_loadweight -= _containers[oldSerialNumber].Mass;
					_loadweight += newContainer.Mass;
					_containers.Remove(oldSerialNumber);
					_containers[newContainer.SerialNumber] = newContainer;
				}
				else {
					throw new ArgumentException("No container found with the specified serial number.");
				}
			}
			public string DescribeBoat() {
				StringBuilder toRet = new StringBuilder();
				toRet.Append($"{GetType().Name}: Capacity - {_capacity}/{_maxCapacity}, Total Mass - {_loadweight}/{_maxLoadWeight}, Max Speed - {_maxSpeed}");
				toRet.Append("Containers:");
				foreach (var container in _containers.Values) {
					toRet.Append(container.Describe());
				}
				return toRet.ToString();
			}
			public static void ChangeContainter(Boat from, Boat to, string serialNumber) {
				Container container = from.DeLoadContainer(serialNumber);
				to.LoadBoat(container);
			}
			private int _capacity = 0;
			private int _maxCapacity;
			private int _loadweight = 0;
			private int _maxLoadWeight;
			private int _maxSpeed;
			private Dictionary<string, Container> _containers = new Dictionary<string, Container>();
		}
	}
}