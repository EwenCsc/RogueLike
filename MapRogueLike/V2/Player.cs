using System;
using System.Collections.Generic;
using MapRogueLike.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapRogueLike
{

    public class Player
    {
        public enum PlayerAnim
        {
            Down, Up, Right, Left,
            DownIdle, UpIdle, RightIdle, LeftIdle
        }

        int life = 0;
        int maxLife = 100;

        int stamina = 0;
        int maxStamina = 100;

        Animator<PlayerAnim> animator;

        Vector2 velocity = Vector2.Zero;

        Room currentRoom;
        private float moveSpeed = 4.0f;

        public Vector2 Position { get; set; }

        public Player()
        {
            animator = new Animator<PlayerAnim>(new Dictionary<PlayerAnim, Animation>()
            {
                { PlayerAnim.RightIdle, (Animation)ToolBox.Instance.Get<AssetManager>().DrawableAssets["RightIdle_Barbare"] },
                {  PlayerAnim.DownIdle, (Animation)ToolBox.Instance.Get<AssetManager>().DrawableAssets["DownIdle_Barbare"]  },
                {  PlayerAnim.LeftIdle, (Animation)ToolBox.Instance.Get<AssetManager>().DrawableAssets["LeftIdle_Barbare"]  },
                {    PlayerAnim.UpIdle, (Animation)ToolBox.Instance.Get<AssetManager>().DrawableAssets["UpIdle_Barbare"]    },
                {     PlayerAnim.Right, (Animation)ToolBox.Instance.Get<AssetManager>().DrawableAssets["Right_Barbare"]     },
                {      PlayerAnim.Down, (Animation)ToolBox.Instance.Get<AssetManager>().DrawableAssets["Down_Barbare"]      },
                {      PlayerAnim.Left, (Animation)ToolBox.Instance.Get<AssetManager>().DrawableAssets["Left_Barbare"]      },
                {        PlayerAnim.Up, (Animation)ToolBox.Instance.Get<AssetManager>().DrawableAssets["Up_Barbare"]        },
            });
            life = maxLife;
            stamina = maxStamina;
        }

        public void Update(GameTime gameTime)
        {
            Movements();
            AnimationUpdate();
        }

        private void Movements()
        {
            Vector2 v = Vector2.Zero;
            if (Input.GetKey(KeyBinds.MovevementUp))
            {
                v += new Vector2(0, -moveSpeed);
            }
            if (Input.GetKey(KeyBinds.MovevementDown))
            {
                v += new Vector2(0, moveSpeed);
            }
            if (Input.GetKey(KeyBinds.MovevementLeft))
            {
                v += new Vector2(-moveSpeed, 0);
            }
            if (Input.GetKey(KeyBinds.MovevementRight))
            {
                v += new Vector2(moveSpeed, 0);
            }

            if (v != Vector2.Zero)
            {
                v.Normalize();
            }
            velocity = v * moveSpeed;
            Position += velocity;
        }

        private void AnimationUpdate()
        {
            UpdateDirection();
            animator.Update();
        }

        private void UpdateDirection()
        {
            if (velocity == Vector2.Zero)
            {
                switch (animator.CurrentAnimationState)
                {
                    case PlayerAnim.Down:
                        animator.SetAnim(PlayerAnim.DownIdle);
                        break;
                    case PlayerAnim.Up:
                        animator.SetAnim(PlayerAnim.UpIdle);
                        break;
                    case PlayerAnim.Right:
                        animator.SetAnim(PlayerAnim.RightIdle);
                        break;
                    case PlayerAnim.Left:
                        animator.SetAnim(PlayerAnim.LeftIdle);
                        break;
                    default: break;
                }
            }
            else
            {
                if (velocity.Y < 0)
                {
                    animator.SetAnim(PlayerAnim.Up);
                }
                else if (velocity.Y > 0)
                {
                    animator.SetAnim(PlayerAnim.Down);
                }
                else if (velocity.X < 0)
                {
                    animator.SetAnim(PlayerAnim.Left);
                }
                else if (velocity.X > 0)
                {
                    animator.SetAnim(PlayerAnim.Right);
                }
            }
        }

        //private float scale = 1;
        public void Draw(SpriteBatch spriteBatch)
        {
            //Tests
            //if (Input.GetKey(Microsoft.Xna.Framework.Input.Keys.A))
            //{
            //    scale = MathHelper.Clamp(scale + 0.1f, 0.1f, 1000);
            //}
            //if (Input.GetKey(Microsoft.Xna.Framework.Input.Keys.E))
            //{
            //    scale = MathHelper.Clamp(scale - 0.1f, 0.1f, 1000);
            //}
            spriteBatch.Draw(animator.CurrentAnimation.GetTexture(), Position, null, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
        }

        public void SetCurrentRoom(Room room)
        {
            currentRoom = room;
        }
    }
}