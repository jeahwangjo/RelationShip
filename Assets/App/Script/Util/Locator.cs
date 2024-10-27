using System;
using System.Collections.Generic;

public static class Locator
{
    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    public static void RegisterService<T>(T service)
    {
        Type serviceType = typeof(T);

        if (!services.ContainsKey(serviceType))
        {
            services.Add(serviceType, service);
        }
        else
        {
            throw new InvalidOperationException($"Service of tuype {serviceType} is ");
        }
    }

    public static T GetService<T>()
    {
        Type serviceType = typeof(T);

        if (services.TryGetValue(serviceType, out var service))
        {
            return (T)service;
        }
        else
        {
            throw new KeyNotFoundException($"service of type{serviceType}not re");
        }
    }


}
