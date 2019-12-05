using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRogueLike.Engine
{
    class Animator<T> where T : struct, IConvertible
    {
        Dictionary<T, Animation> animations = new Dictionary<T, Animation>();
        T currentAnimationState;
        Animation currentAnimation;

        public T CurrentAnimationState => currentAnimationState;
        public Animation CurrentAnimation => currentAnimation;

        public Animator(Dictionary<T, Animation> anims)
        {
            animations = anims;
            currentAnimation = animations.First().Value;
            currentAnimationState = animations.First().Key;
        }

        public void AddAnimation(T key, Animation anim)
        {
            if (animations.ContainsKey(key)) return;
            animations.Add(key, anim);
        }

        public void SetAnim(T key)
        {
            if (!animations.ContainsKey(key) || currentAnimationState.Equals(key)) return;
            currentAnimationState = key;
            currentAnimation = animations[key];
        }

        public void Update()
        {
            currentAnimation.Update();
        }
    }
}
