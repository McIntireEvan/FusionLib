using Microsoft.Xna.Framework;

namespace FusionLib.Utils
{
    /// <summary>
    /// A wrapper class to serve as a static reference to a GameServiceContainer
    /// </summary>
    public static class GameServices
    {
        private static GameServiceContainer container;
        public static GameServiceContainer Instance
        {
            get
            {
                if (container == null)
                {
                    container = new GameServiceContainer();
                }
                return container;
            }
        }

        /// <summary>
        /// Gets the service of type T
        /// </summary>
        /// <typeparam name="T">Type of service to get</typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return (T)Instance.GetService(typeof(T));
        }

        /// <summary>
        /// Adds a service 'service' of type T
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <param name="service">Service</param>
        public static void AddService<T>(T service)
        {
            Instance.AddService(typeof(T), service);
        }

        /// <summary>
        /// Removes the service stored for type T
        /// </summary>
        /// <typeparam name="T">Type to remove</typeparam>
        public static void RemoveService<T>()
        {
            Instance.RemoveService(typeof(T));
        }
    }
}