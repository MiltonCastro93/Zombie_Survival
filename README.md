# 🧟‍♂️ Zombie Survival (Attack on Humans!)

> Proyecto académico inspirado en el modo *Zombies* de **Call of Duty: Black Ops**, reimaginado con una estética low-poly y mecánicas propias.  
> Desarrollado con **Unity 2024** y **C#**.

---

## 🧠 Descripción general

**Zombie Survival**, también conocido como **Attack on Humans!**, es un **prototipo de supervivencia en primera persona** en el que el jugador debe resistir oleadas de enemigos en un entorno urbano destruido.  
El proyecto busca **replicar la tensión y progresión del modo zombie clásico**, pero adaptado a una dirección artística más liviana y estilizada.

El desarrollo se centró en el **diseño de sistemas de oleadas, IA enemiga, control de armas y HUD dinámico**, explorando la implementación moderna de estas mecánicas en **Unity 2024**.

---

## ⚙️ Detalles técnicos

| Elemento | Descripción |
|-----------|--------------|
| 🧩 **Motor** | Unity 2024 (versión LTS) |
| 💻 **Lenguaje** | C# |
| 🎮 **Tipo de proyecto** | Académico / Prototipo jugable |
| 🧠 **Inspiración** | *Call of Duty: Black Ops – Zombies* |
| 🎨 **Estética visual** | Low-poly, estilo voxel |
| 🧱 **Plataforma** | Windows |
| 💾 **Control de versiones** | Git / GitHub |
| 👤 **Desarrollador** | Milton Castro |

---

## 🔫 Mecánicas principales

- **Sistema de oleadas dinámicas:** los enemigos aparecen progresivamente con mayor dificultad y velocidad.  
- **Detección de daño y puntuación:** el jugador gana puntos por eliminar zombis, con contador visible en pantalla.  
- **HUD funcional:** muestra información de **ronda, puntos, munición y salud (HP)** en tiempo real.  
- **IA enemiga:** los zombis persiguen activamente al jugador utilizando *NavMesh* y detección de proximidad.  
- **Sistema de recarga y gestión de armas:** basado en estados internos y animaciones sincronizadas.  
- **Diseño modular:** scripts independientes para IA, disparo, puntuación, y generación de oleadas.

---

## 🖼️ Capturas del proyecto

_(Imágenes de libre licencia utilizadas con fines académicos)_

![Gameplay 1]()

> *Vista del combate en primera persona, mostrando HUD, contador de rondas y enemigos activos.*

---

## 🧩 Arquitectura del código

- **PlayerController.cs** → Movimiento y control de disparo del jugador.  
- **EnemyAI.cs** → Comportamiento del enemigo: detección, persecución y ataque.  
- **WaveManager.cs** → Sistema de oleadas con incremento progresivo.  
- **UIManager.cs** → Actualización dinámica del HUD (rondas, salud, munición, puntos).  
- **WeaponSystem.cs** → Gestión de armas, disparos y recarga.  

---

## 🎯 Propósito académico

El desarrollo de **Zombie Survival** tuvo como objetivo:
- Implementar **sistemas de combate en tiempo real** con lógica de IA.  
- Comprender el **flujo de gameplay basado en rondas**.  
- Desarrollar **interfaces dinámicas (HUD)** sincronizadas con eventos del juego.  
- Aplicar **principios de optimización** para múltiples entidades simultáneas.  

---

## 📜 Licencia

Este proyecto es de uso **académico y sin fines comerciales**.  
Los recursos visuales y modelos utilizados son **de libre licencia**.

---

## ✨ Cierre

> *“Cada enemigo eliminado no es solo una victoria, sino una nueva oportunidad para mejorar la lógica detrás del juego.”*  
> — Milton Castro
