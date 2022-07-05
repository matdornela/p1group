namespace API.Routes
{
    public static class ApiRoutes
    {
        private static readonly string BaseUrl = "https://localhost:44300/";

        public static class Airports
        {
            private static readonly string _airportsControllerUrl = string.Concat(BaseUrl, "airports");

            public static readonly string GetAll = _airportsControllerUrl;

            public static readonly string Create = string.Concat(_airportsControllerUrl, "/Create");

            public static readonly string Put = _airportsControllerUrl;

            public static readonly string Get = string.Concat(_airportsControllerUrl, "/{Id}");

            public static readonly string Delete = string.Concat(_airportsControllerUrl, "/{Id}");
        }

        public static class Flights
        {
            private static readonly string _flightsControllerUrl = string.Concat(BaseUrl, "flights");

            public static readonly string GetAll = _flightsControllerUrl;

            public static readonly string Create = string.Concat(_flightsControllerUrl, "/Create");

            public static readonly string Put = _flightsControllerUrl;

            public static readonly string Get = string.Concat(_flightsControllerUrl, "/{Id}");

            public static readonly string Delete = string.Concat(_flightsControllerUrl, "/{Id}");
        }

        public static class Orders
        {
            private static readonly string _ordersControllerUrl = string.Concat(BaseUrl, "orders");

            public static readonly string GetAll = _ordersControllerUrl;

            public static readonly string Create = string.Concat(_ordersControllerUrl, "/Create");

            public static readonly string Put = _ordersControllerUrl;

            public static readonly string Get = string.Concat(_ordersControllerUrl, "/{Id}");

            public static readonly string Delete = string.Concat(_ordersControllerUrl, "/{Id}");
        }
    }
}