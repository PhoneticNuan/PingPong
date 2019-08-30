using System.Collections.Generic;

using Eccentric.Collections;
using Eccentric.Utils;

using UnityEngine;
public class PaintballPool : TSingletonMonoBehavior<PaintballPool> {
    public List<ObjectPool> balls = new List<ObjectPool> ( );
    protected override void Awake ( ) {
        foreach (ObjectPool item in balls) {
            item.Init ( );
        }
    }
}
