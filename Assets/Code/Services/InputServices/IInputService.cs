using UnityEngine;

namespace RadioSilence.Services.InputServices
{
    public interface IInputService
    {
        bool IsInUI { get; }
        bool Fire { get; }
        bool Reload { get; }
        bool Interact { get; }
        bool Inventory { get; }
        Vector2 MouseDelta { get; }
        Vector2 MoveDirection { get; }
    }
}