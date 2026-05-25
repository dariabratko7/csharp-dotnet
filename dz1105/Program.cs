using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//Ви працюєте над системою логістики. Завдання - реалізувати систему, яка дозволяє:

//1) Зібрати маршрут доставки з окремих точок (Builder).
//2) Додавати до маршруту особливості: страхування, холодова доставка, терміновість (Decorator).
//3) Дозволяє об'єднувати прості та складові маршрути (Composite).

//Сконструювати гнучку систему маршрутів доставки, де можна легко налаштовувати маршрути, додавати особливості та об'єднувати їх у складніші ланцюжки.

//Вимоги:

//Builder - для поетапної побудови маршруту: додавання точок, встановлення типу транспорту та ін.
//Decorator - для "обгортання" маршруту додатковими опціями (InsuredRouteDecorator, RefrigeratedRouteDecorato, rExpressRouteDecorator).

//Composite - підтримка складових маршрутів (singleRoute — одна ділянка compositeRoute — набір маршрутів як єдиний маршрут).

//Орієнтовна структура класів:

//IRoute — інтерфейс маршруту (CalculateCost(), Describe()).
//SingleRoute — простий маршрут між двома точками.
//CompositeRoute — об'єднання кількох маршрутів.
//RouteBuilder — для створення маршрутів покроково.
//RouteDecorator (абстрактний), та його реалізації: (InsuredRoute ExpressRoute RefrigeratedRoute).


namespace LogisticsSystem
{
 

    public interface IRoute
    {
        double CalculateCost();
        string Describe();
    }
 

    public class SingleRoute : IRoute
    {
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public double Distance { get; set; }

        public SingleRoute(string from, string to, string transportType, double distance)
        {
            From = from;
            To = to;
            TransportType = transportType;
            Distance = distance;
        }

        public double CalculateCost()
        {
            double rate = TransportType switch
            {
                "Truck" => 2.0,
                "Plane" => 5.0,
                "Ship" => 1.5,
                _ => 2.0
            };

            return Distance * rate;
        }

        public string Describe()
        {
            return $"Маршрут: {From} -> {To}, Транспорт: {TransportType}, Відстань: {Distance} км";
        }
    }
 

    public class CompositeRoute : IRoute
    {
        private List<IRoute> routes = new List<IRoute>();

        public void AddRoute(IRoute route)
        {
            routes.Add(route);
        }

        public void RemoveRoute(IRoute route)
        {
            routes.Remove(route);
        }

        public double CalculateCost()
        {
            return routes.Sum(r => r.CalculateCost());
        }

        public string Describe()
        {
            string result = "Складений маршрут:\n";

            foreach (var route in routes)
            {
                result += " - " + route.Describe() + "\n";
            }

            return result;
        }
    }
 

    public class RouteBuilder
    {
        private string from;
        private string to;
        private string transportType;
        private double distance;

        public RouteBuilder SetStartPoint(string from)
        {
            this.from = from;
            return this;
        }

        public RouteBuilder SetEndPoint(string to)
        {
            this.to = to;
            return this;
        }

        public RouteBuilder SetTransport(string transport)
        {
            transportType = transport;
            return this;
        }

        public RouteBuilder SetDistance(double dist)
        {
            distance = dist;
            return this;
        }

        public SingleRoute Build()
        {
            return new SingleRoute(from, to, transportType, distance);
        }
    }

    public abstract class RouteDecorator : IRoute
    {
        protected IRoute route;

        public RouteDecorator(IRoute route)
        {
            this.route = route;
        }

        public virtual double CalculateCost()
        {
            return route.CalculateCost();
        }

        public virtual string Describe()
        {
            return route.Describe();
        }
    }


    public class InsuredRouteDecorator : RouteDecorator
    {
        public InsuredRouteDecorator(IRoute route) : base(route)
        {
        }

        public override double CalculateCost()
        {
            return base.CalculateCost() + 100;
        }

        public override string Describe()
        {
            return base.Describe() + " + Страхування";
        }
    }


    public class ExpressRouteDecorator : RouteDecorator
    {
        public ExpressRouteDecorator(IRoute route) : base(route)
        {
        }

        public override double CalculateCost()
        {
            return base.CalculateCost() * 1.5;
        }

        public override string Describe()
        {
            return base.Describe() + " + Термінова доставка";
        }
    }


    public class RefrigeratedRouteDecorator : RouteDecorator
    {
        public RefrigeratedRouteDecorator(IRoute route) : base(route)
        {
        }

        public override double CalculateCost()
        {
            return base.CalculateCost() + 250;
        }

        public override string Describe()
        {
            return base.Describe() + " + Холодова доставка";
        }
    }

  

    class Program
    {
        static void Main(string[] args)
        { 
            IRoute route1 = new RouteBuilder()
                .SetStartPoint("Одеса")
                .SetEndPoint("Київ")
                .SetTransport("Truck")
                .SetDistance(480)
                .Build();

            IRoute route2 = new RouteBuilder()
                .SetStartPoint("Київ")
                .SetEndPoint("Львів")
                .SetTransport("Plane")
                .SetDistance(540)
                .Build();
 
            route1 = new InsuredRouteDecorator(route1);
            route1 = new ExpressRouteDecorator(route1);

            route2 = new RefrigeratedRouteDecorator(route2);
 
            CompositeRoute fullRoute = new CompositeRoute();
            fullRoute.AddRoute(route1);
            fullRoute.AddRoute(route2);
 
            Console.WriteLine(fullRoute.Describe());

            Console.WriteLine("--------------------------------");

            Console.WriteLine($"Загальна вартість: {fullRoute.CalculateCost()} грн");
        }
    }
}

//Реалізувати систему керування телевізором, яка використовуватиме два патерни: 
//Command: для інкапсуляції команд, таких як увімкнення телевізора, вимкнення та зміна каналу. 
//State: для представлення різних станів телевізора, таких як увімкнено або вимкнено.

//Команди:
//1) Turn On Command: Команда для увімкнення телевізора. 
//2) Turn Off Command: Команда вимкнення телевізора. 
//3) Change Channel Command: Команда зміни каналу. 
//4) Increase Volume Command: Команда збільшення гучності. 

namespace TvControlSystem
{
 

    public interface ITvState
    {
        void TurnOn(TV tv);
        void TurnOff(TV tv);
        void ChangeChannel(TV tv, int channel);
        void IncreaseVolume(TV tv);
    }

    public class TvOnState : ITvState
    {
        public void TurnOn(TV tv)
        {
            Console.WriteLine("Телевізор вже увімкнений.");
        }

        public void TurnOff(TV tv)
        {
            Console.WriteLine("Вимикаємо телевізор...");
            tv.State = new TvOffState();
        }

        public void ChangeChannel(TV tv, int channel)
        {
            tv.Channel = channel;
            Console.WriteLine($"Канал змінено на {channel}");
        }

        public void IncreaseVolume(TV tv)
        {
            tv.Volume++;
            Console.WriteLine($"Гучність: {tv.Volume}");
        }
    }

    public class TvOffState : ITvState
    {
        public void TurnOn(TV tv)
        {
            Console.WriteLine("Увімкнення телевізора...");
            tv.State = new TvOnState();
        }

        public void TurnOff(TV tv)
        {
            Console.WriteLine("Телевізор вже вимкнений.");
        }

        public void ChangeChannel(TV tv, int channel)
        {
            Console.WriteLine("Неможливо змінити канал. Телевізор вимкнений.");
        }

        public void IncreaseVolume(TV tv)
        {
            Console.WriteLine("Неможливо змінити гучність. Телевізор вимкнений.");
        }
    }
 

    public class TV
    {
        public ITvState State { get; set; }
        public int Channel { get; set; } = 1;
        public int Volume { get; set; } = 10;

        public TV()
        {
            State = new TvOffState();
        }

        public void TurnOn() => State.TurnOn(this);
        public void TurnOff() => State.TurnOff(this);
        public void ChangeChannel(int channel) => State.ChangeChannel(this, channel);
        public void IncreaseVolume() => State.IncreaseVolume(this);
    }
 
    public interface ICommand
    {
        void Execute();
    }

    public class TurnOnCommand : ICommand
    {
        private TV tv;

        public TurnOnCommand(TV tv)
        {
            this.tv = tv;
        }

        public void Execute()
        {
            tv.TurnOn();
        }
    }

    public class TurnOffCommand : ICommand
    {
        private TV tv;

        public TurnOffCommand(TV tv)
        {
            this.tv = tv;
        }

        public void Execute()
        {
            tv.TurnOff();
        }
    }

    public class ChangeChannelCommand : ICommand
    {
        private TV tv;
        private int channel;

        public ChangeChannelCommand(TV tv, int channel)
        {
            this.tv = tv;
            this.channel = channel;
        }

        public void Execute()
        {
            tv.ChangeChannel(channel);
        }
    }

    public class IncreaseVolumeCommand : ICommand
    {
        private TV tv;

        public IncreaseVolumeCommand(TV tv)
        {
            this.tv = tv;
        }

        public void Execute()
        {
            tv.IncreaseVolume();
        }
    }
 

    public class RemoteControl
    {
        public void Submit(ICommand command)
        {
            command.Execute();
        }
    }
 

    class Program
    {
        static void Main(string[] args)
        {
            TV tv = new TV();
            RemoteControl remote = new RemoteControl();

            ICommand turnOn = new TurnOnCommand(tv);
            ICommand changeChannel = new ChangeChannelCommand(tv, 5);
            ICommand volumeUp = new IncreaseVolumeCommand(tv);
            ICommand turnOff = new TurnOffCommand(tv);

            remote.Submit(turnOn);
            remote.Submit(changeChannel);
            remote.Submit(volumeUp);
            remote.Submit(volumeUp);

            remote.Submit(turnOff);
 
            remote.Submit(changeChannel);
            remote.Submit(volumeUp);
        }
    }
}