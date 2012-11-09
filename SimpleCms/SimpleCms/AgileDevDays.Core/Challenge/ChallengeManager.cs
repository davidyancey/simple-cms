using System.Collections.Generic;
using System.Linq;
using AgileDevDays.Infrastructure;
using AgileDevDays.Infrastructure.Entities;
using AgileDevDays.Infrastructure.Models;
using AutoMapper;

namespace AgileDevDays.Core.Challenge
{
    public class ChallengeManager
    {
        private readonly ChallengeEntities _context = new ChallengeEntities();
        private readonly Repository<ChallengeEntities> _repository;

        public ChallengeManager()
        {
            _repository = new Repository<ChallengeEntities>(_context);
        }
        public Dictionary<string, List<ChallengeEntry>> GetChallenges()
        {
            var challenges = _repository.Get<ChallengeEntity>().ToList();
            var results = new Dictionary<string, List<ChallengeEntry>>();
            foreach(var item in challenges)
            {
                var challengetype = item.ChallengeType;
                if (results.ContainsKey(challengetype))
                    continue;
                results.Add(challengetype, Mapper.Map<List<ChallengeEntry>>(challenges.Where(x => x.ChallengeType == challengetype).ToList()));
            }
            return results;
        }

        public List<ChallengeEntry> GetChallenges(string challengeType)
        {
            return Mapper.Map<List<ChallengeEntry>>(_repository.Get<ChallengeEntity>(x => x.ChallengeType == challengeType).ToList());
        }

        public ChallengeEntry GetChallenge(int challengeId)
        {
            var challenge = _repository.GetSingle<ChallengeEntity>(x => x.ChallengeId == challengeId);
            return Mapper.Map<ChallengeEntry>(challenge);
        }
    }
}