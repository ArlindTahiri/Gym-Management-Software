
namespace loremipsum.Gym
{
    public class GymFactory
    {
        private readonly Impl.Gym gym;

        public GymFactory(IGymPersistence persistence)
        {
            gym = new Impl.Gym(persistence);
        }

        public IProductAdmin GetProductAdmin()
        {
            return gym;
        }

        public IProductModule GetProductModule()
        {
            return gym;
        }
    }
}
