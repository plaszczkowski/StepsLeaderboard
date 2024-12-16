using StepsLeaderboard.Entities;

namespace StepsLeaderboard.Data
{
    public class InMemoryDataStore
    {
        private readonly List<Team> _teams = new List<Team>();
        private readonly List<Counter> _counters = new List<Counter>();
        private int _counterId = 1;
        private int _teamId = 1;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public InMemoryDataStore()
        {
            // Initial team for testing
            AddTeam(new Team { Name = "Initial Team" });
        }

        public List<Team> GetTeams()
        {
            _lock.EnterReadLock();
            try
            {
                return _teams.ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public Team? GetTeam(int id)
        {
            _lock.EnterReadLock();
            try
            {
                return _teams.FirstOrDefault(t => t.Id == id);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public Counter? GetCounter(int id)
        {
            _lock.EnterReadLock();
            try
            {
                return _counters.FirstOrDefault(c => c.Id == id);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void AddCounter(Counter counter)
        {
            _lock.EnterWriteLock();
            try
            {
                counter.Id = _counterId++;
                _counters.Add(counter);
                var team = _teams.FirstOrDefault(t => t.Id == counter.TeamId);
                team?.Counters.Add(counter);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void DeleteCounter(int id)
        {
            _lock.EnterWriteLock();
            try
            {
                var counter = _counters.FirstOrDefault(c => c.Id == id);
                if (counter != null)
                {
                    _counters.Remove(counter);
                    var team = _teams.FirstOrDefault(t => t.Id == counter.TeamId);
                    team?.Counters.Remove(counter);
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void AddTeam(Team team)
        {
            _lock.EnterWriteLock();
            try
            {
                team.Id = _teamId++;
                _teams.Add(team);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void DeleteTeam(int id)
        {
            _lock.EnterWriteLock();
            try
            {
                var team = _teams.FirstOrDefault(t => t.Id == id);
                if (team != null)
                {
                    _teams.Remove(team);
                    _counters.RemoveAll(c => c.TeamId == id);
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
