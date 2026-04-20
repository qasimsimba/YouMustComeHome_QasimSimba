# YouMustComeHome_QasimSimba
Project: Atmospheric Horror / Narrative Simulation

Focus: Visual Identity, Recursive Dialogue, and Environment Interaction

I. Design Evolution & Visual Philosophy

In the transition from Alpha to the current build, the most significant design shift was the commitment to a high-contrast, monochromatic (Black and White) aesthetic. This was a deliberate choice to align with the game’s themes of existential dread and the "uncanny" nature of the forest setting.

The Irish Deer: Based on the design document, the Irish Deer is no longer a static NPC. It has been redesigned as a focal point of visual contrast. While the forest is rendered in deep blacks and charcoal greys, the Deer utilizes a high-intensity white emission shader to make it feel otherworldly and non-native to the environment.

The Forest Scene: The forest was modified from a traditional 3D space to a layered, reactive environment. I shifted from standard lighting to a Global Volumetric Fog system that limits the player’s vision, forcing a reliance on sound and the "shimmer" of shaders to navigate.

II. Custom Shader Implementation
The shaders are the primary storytelling device in You Must Come Home, replacing traditional UI with environmental cues.
1. The "Forest Pulse" (Vertex Displacement)
To make the forest feel alive yet hostile, I implemented a vertex shader on all foliage. Unlike standard wind shaders, this movement is tied to the player's proximity and current "Stress" level.

Modification: The trees subtly lean away or toward the player based on dialogue choices, using a _Strength parameter controlled via C# scripts.

2. Black & White Post-Processing
Instead of a simple grayscale filter, I developed a custom K-Means Clustering Shader that crushes the color palette into distinct levels of black, white, and one specific shade of grey. This ensures the "Irish Deer" always remains the brightest object on screen, naturally drawing player focus.

III. Dialogue & Narrative Logic
The dialogue system was overhauled to move away from linear scripts toward a State-Aware Dialogue Engine.

Visual Dialogue and Movement
The player can click to move through the envirment instead of WASD. Also the dialogue boxed appear to tell the story, and they can click through them while navigating the scene

Added Music
Added sound effects, guitar playing in the first scene. Wind in scene 2. And spooky music for scene 3.
