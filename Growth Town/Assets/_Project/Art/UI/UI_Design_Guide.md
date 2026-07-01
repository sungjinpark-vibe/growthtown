# Life Town - UI Design Guide

## 1. Concept: Bubbly & Jelly-like
The core aesthetic of Life Town's UI is "Bubbly". All interactive elements should feel like soft, bouncy jelly. They should invite the user to touch them, giving a fun and accessible vibe.

## 2. Shapes & Corner Radius
- **Buttons**: Fully rounded corners (Pill shape) or highly rounded rectangles. Minimum corner radius should be **50% of the element's height**, or at least **24px** for smaller elements.
- **Panels/Popups**: Soft corners with a minimum radius of **32px**. Avoid sharp edges entirely to maintain the friendly look.

## 3. Shading & 3D Effect
To achieve the jelly-like 3D volume, use the following shading techniques:
- **Base Color**: Vibrant, pastel or soft neon colors.
- **Inner Glow (Top Edge)**: Add a soft white/light inner shadow at the top to simulate light reflecting off a glossy, rounded surface (Opacity: 40-60%).
- **Inner Shadow (Bottom Edge)**: Add a darker, saturated inner shadow at the bottom to give depth and volume (Opacity: 20-30%).
- **Drop Shadow**: Soft, blurred drop shadow positioned slightly downwards (Y-offset). **Use a color matching the button's base color** (instead of standard black) to simulate light passing through the jelly material.

## 4. Animation (Bouncy)
Motion is critical to the Bubbly theme.
- **Hover**: Slight scale up (e.g., 1.05x).
- **Click/Press**: Squish effect (scale down Y, slight scale up X) to simulate softness, then bounce back up using elastic or overshoot easing.
