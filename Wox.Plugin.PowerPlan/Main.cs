using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wox.Plugin.PowerPlan
{
    public class Main : IPlugin
    {
        private static string currentPath;
        public void Init(PluginInitContext context)
        {
            currentPath = context.CurrentPluginMetadata.PluginDirecotry;
        }

        public List<Result> Query(Query query)
        {
            var results = new List<Result>();
            var powerList = PowerManager.GetAll();
            results = powerList.Select(p => new Result() {
                Title = p.Name,
                SubTitle = p.Name,
                IcoPath = System.IO.Path.Combine(currentPath, p.IsActive ? @"ico\Battery-2.png" : @"ico\Battery-1.png"),
                Action = (a) => {
                    PowerManager.Active(p.Id);
                    return true;
                }
            }).ToList();
            if (query.ActionParameters.Any()) {
                var keyword = query.ActionParameters[0].ToLower();
                results = results.Where(r => r.Title.ToLower().Contains(keyword)).ToList();
            }
            return results;
        }
    }
}
