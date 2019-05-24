﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robust.Client.Graphics.Drawing;
using Robust.Client.Interfaces.GameObjects.Components;
using Robust.Shared.Log;
using Robust.Shared.Maths;

namespace Robust.Client.UserInterface.Controls
{
    public class SpriteView : Control
    {
        ISpriteProxy Mirror;
        ISpriteComponent _sprite;

        public ISpriteComponent Sprite
        {
            get => _sprite;
            set
            {
                _sprite = value;
                if (Mirror != null)
                {
                    Mirror.Dispose();
                    Mirror = null;
                }

                if (value != null)
                {
                    Mirror = value.CreateProxy();
                    UpdateMirrorPosition();
                }
            }
        }

        public SpriteView() : base()
        {
        }

        public SpriteView(string name) : base(name)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            RectClipContent = true;
        }

        protected override void Resized()
        {
            base.Resized();
            UpdateMirrorPosition();
        }

        void UpdateMirrorPosition()
        {
            if (Mirror == null)
            {
                return;
            }

            Mirror.Offset = Size / 2;
        }

        protected override Vector2 CalculateMinimumSize()
        {
            // TODO: make this not hardcoded.
            // It'll break on larger things.
            return new Vector2(32, 32);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Mirror?.Dispose();
        }

        protected internal override void Draw(DrawingHandleScreen handle)
        {
            if (_sprite == null)
            {
                return;
            }

            handle.DrawEntity(_sprite.Owner, GlobalPixelPosition + PixelSize / 2);
        }
    }
}