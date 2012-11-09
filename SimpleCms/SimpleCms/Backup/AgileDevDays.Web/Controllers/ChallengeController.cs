using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgileDevDays.Core.Challenge;
using AgileDevDays.Web.Models;
using AutoMapper;

namespace AgileDevDays.Web.Controllers
{
    public class ChallengeController : BaseController
    {
        //
        // GET: /Challenge/
        private readonly ChallengeManager _manager = new ChallengeManager();

        public ActionResult Index()
        {
            ChallengeListModel model = new ChallengeListModel();
            var challenges = _manager.GetChallenges();
            var mappedList = new Dictionary<string, List<ChallengeModel>>();
            foreach(var item in challenges)
            {
                if (mappedList.ContainsKey(item.Key))
                    continue;
                mappedList.Add(item.Key, Mapper.Map<List<ChallengeModel>>(item.Value));
            }
            model.Challenges = mappedList;
            model.Title = "Challenges";
            return View(model);
        }

        public ActionResult Content(int challengeId = 0)
        {
            challengeId = 1;
            if (challengeId == 0)
                return RedirectToAction("Index");
            var challenge = _manager.GetChallenge(challengeId);
            ChallengeModel model = Mapper.Map<ChallengeModel>(challenge);

            return View(model);
        }
    }
}
