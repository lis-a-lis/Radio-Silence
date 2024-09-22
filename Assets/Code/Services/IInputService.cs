using UnityEngine;

namespace RadioSilence.Services
{
    public interface IInputService
    {
        bool Fire { get; }
        bool Reload { get; }
        bool Interact { get; }
        bool Inventory { get; }
        Vector2 MouseDelta { get; }
        Vector2 MoveDirection { get; }
    }
}