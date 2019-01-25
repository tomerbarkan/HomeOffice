using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHitable {
    void Hit(Throwable throwable);
}
