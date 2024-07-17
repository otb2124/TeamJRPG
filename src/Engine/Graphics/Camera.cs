using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TeamJRPG
{
    public class Camera
    {
        public Vector2 position;
        public float zoom;
        private float rotation;
        private Matrix transform;
        public Viewport viewport;
        public readonly float DEFAULT_ZOOM = 1.0f;
        public readonly float MIN_ZOOM = 0.85f, MAX_ZOOM = 5f;
        public readonly Vector2 MAX_DISTANCE_FROM_PLAYER = new Vector2(Globals.graphics.PreferredBackBufferWidth / 2, Globals.graphics.PreferredBackBufferHeight / 2);
        private readonly Vector2 maxDistanceFromCenter;
        private Vector2 targetPosition;
        private Vector2 playerOffset;

        public bool FollowPlayer = false;
        private readonly float transitionSpeed = 0.1f; // Adjust the speed of the transition as necessary

        public Entity focusEntity;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
            maxDistanceFromCenter = Globals.tileSize * 3; // 3 tiles threshold
            Init();
        }

        public void Init()
        {
            zoom = DEFAULT_ZOOM;
            rotation = 0f;
            playerOffset = Vector2.Zero;
            targetPosition = Vector2.Zero;
        }

        public void Reload()
        {
            if (Globals.currentGameState == Globals.GameState.playstate)
            {
                position = Globals.player.position;
                ClampTargetPosition();
            }
            else if(Globals.currentGameState == Globals.GameState.mainmenustate)
            {
                position = Vector2.Zero;
            }
            else if(Globals.currentGameState == Globals.GameState.battle)
            {
                position.X = Globals.battleManager.background.foreGroundWidth/2;
                position.Y = viewport.Height / 2;
                ClampBattlePosition();
            }

            targetPosition = position;

            UpdateTransform();
        }

        public void Update()
        {
            if (Globals.currentGameState == Globals.GameState.battle)
            {
                if (Globals.inputManager.IsKeyPressed(Keys.Left)) { position += new Vector2(-5, 0); }
                if (Globals.inputManager.IsKeyPressed(Keys.Right)) { position += new Vector2(5, 0); }
                ClampBattlePosition();
            }
            else
            {
                HandleManualMovement();

                if (Globals.inputManager.IsKeyPressed(Keys.OemPlus)) Zoom(0.05f);
                if (Globals.inputManager.IsKeyPressed(Keys.OemMinus)) Zoom(-0.05f);

                if (Globals.inputManager.IsKeyPressedAndReleased(Keys.OemTilde))
                {
                    FollowPlayer = !FollowPlayer;
                    if (FollowPlayer)
                    {
                        targetPosition = Globals.player.position;
                    }
                }

                if (FollowPlayer)
                {
                    FollowPlayerWithThreshold();
                }

                ClampTargetPosition();
                SmoothMoveToTarget();
            }

            UpdateTransform();
        }

        private void HandleManualMovement()
        {
            bool manualMove = false;

            if (Globals.inputManager.IsKeyPressed(Keys.Left)) { targetPosition += new Vector2(-5, 0); manualMove = true; }
            if (Globals.inputManager.IsKeyPressed(Keys.Right)) { targetPosition += new Vector2(5, 0); manualMove = true; }
            if (Globals.inputManager.IsKeyPressed(Keys.Up)) { targetPosition += new Vector2(0, -5); manualMove = true; }
            if (Globals.inputManager.IsKeyPressed(Keys.Down)) { targetPosition += new Vector2(0, 5); manualMove = true; }

            if (manualMove)
            {
                FollowPlayer = false;
            }
        }

        private void FollowPlayerWithThreshold()
        {
            // Calculate the offset from the player's position to the camera's position
            playerOffset = Globals.player.position - position;

            // If the player moves beyond the threshold, update the camera position
            if (Math.Abs(playerOffset.X) > maxDistanceFromCenter.X)
            {
                targetPosition.X = Globals.player.position.X - Math.Sign(playerOffset.X) * maxDistanceFromCenter.X;
            }
            if (Math.Abs(playerOffset.Y) > maxDistanceFromCenter.Y)
            {
                targetPosition.Y = Globals.player.position.Y - Math.Sign(playerOffset.Y) * maxDistanceFromCenter.Y;
            }
        }

        private void SmoothMoveToTarget()
        {
            position = Vector2.Lerp(position, targetPosition, transitionSpeed); // Adjust the lerp factor as needed
        }

        public void FocusOn(Vector2 targetPosition)
        {
            this.targetPosition = targetPosition;
            ClampTargetPosition();
            SmoothMoveToTarget();
            UpdateTransform();
        }

        public void FocusOnEntity()
        {
            FocusOn(focusEntity.position);
        }

        public void Zoom(float delta)
        {
            zoom += delta;
            zoom = MathHelper.Clamp(zoom, MIN_ZOOM, MAX_ZOOM);
        }

        public Matrix Transform => transform;

        private void ClampTargetPosition()
        {
            float cameraWidth = viewport.Width / zoom;
            float cameraHeight = viewport.Height / zoom;

            targetPosition.X = MathHelper.Clamp(targetPosition.X, cameraWidth / 2, Globals.currentMap.mapSize.X * Globals.tileSize.X - cameraWidth / 2);
            targetPosition.Y = MathHelper.Clamp(targetPosition.Y, cameraHeight / 2, Globals.currentMap.mapSize.Y * Globals.tileSize.Y - cameraHeight / 2);

            if (!FollowPlayer && Globals.currentGameState != Globals.GameState.battle)
            {
                Vector2 playerPosition = Globals.player.position;
                targetPosition.X = MathHelper.Clamp(targetPosition.X, playerPosition.X - MAX_DISTANCE_FROM_PLAYER.X, playerPosition.X + MAX_DISTANCE_FROM_PLAYER.X);
                targetPosition.Y = MathHelper.Clamp(targetPosition.Y, playerPosition.Y - MAX_DISTANCE_FROM_PLAYER.Y, playerPosition.Y + MAX_DISTANCE_FROM_PLAYER.Y);
            }
        }


        private void ClampBattlePosition()
        {
            float cameraWidth = viewport.Width / zoom;
            float cameraHeight = viewport.Height / zoom;

            // Ensure camera stays within the bounds defined by the background and foreground layers
            float minX = viewport.Width/2;
            float maxX = Globals.battleManager.background.foreGroundWidth - viewport.Width/2;
            float minY = cameraHeight / 2;
            float maxY = Globals.currentMap.mapSize.Y * Globals.tileSize.Y - cameraHeight / 2;

            position.X = MathHelper.Clamp(position.X, minX, maxX);
            position.Y = MathHelper.Clamp(position.Y, minY, maxY);
        }

        private void UpdateTransform()
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-position, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(zoom) *
                Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
        }
    }
}
