using System;

namespace Jaya.Ui.Services
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Service: Attribute
    {
        readonly Type[] _dependentOn;

        public Service(params Type[] dependentOn)
        {
            _dependentOn = dependentOn;
        }

    }
}
