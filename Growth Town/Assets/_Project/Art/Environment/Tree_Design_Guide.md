# Life Town - Tree Design Guide

## 1. Overview
- **Style:** Fortune City Style, Low-Poly
- **Color Palette:** Pastel Tones
- **Size/Grid:** 1x1 Tile

## 2. Tree Types

### 1) Apple Tree (사과나무)
- **Shape:** Rounded canopy with small spherical apples attached.
- **Trunk:** Simple cylindrical shape with a slight taper towards the top.
- **Colors:**
  - Leaves: Pastel Light Green (`#A8E6CF`)
  - Apples: Pastel Red (`#FF8B94`)
  - Trunk: Warm Light Brown (`#D4A5A5`)

### 2) Maple Tree (단풍나무)
- **Shape:** Slightly sharper, tiered low-poly canopy.
- **Trunk:** Straight and sturdy.
- **Colors:**
  - Leaves: Pastel Orange/Yellow (`#FFD3B6` / `#FFAAA5`)
  - Trunk: Warm Light Brown (`#D4A5A5`)

### 3) Round Bush (둥근 덤불)
- **Shape:** Sits directly on the ground (no visible trunk). 1-2 merged spherical/icosahedron low-poly shapes.
- **Colors:**
  - Leaves: Pastel Mint Green (`#DCEDC1`)

## 3. Material Settings (Unity URP)
- **Shader:** Universal Render Pipeline/Lit
- **Workflow Mode:** Metallic
- **Base Map:** None (Use solid colors)
- **Surface Inputs:**
  - Base Color: (Use HEX codes above)
  - Metallic: `0.0` (Matte finish)
  - Smoothness: `0.1` (Low reflectiveness for a soft pastel look)
- **Lighting:** Enable baked global illumination for soft pastel shadows.
