using Core.Police;

namespace Core.Path
{
    public interface IPathManager
    {
        void AddPolice(PoliceBehaviour police, PatrolPath path);
        void UpdatePath(PoliceBehaviour police);
    }
}