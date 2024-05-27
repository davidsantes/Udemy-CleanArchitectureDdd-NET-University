# Clean architecture y Domain Driven Design - avanzado

Ejercicios tomados del curso de .Net University en Udemy: **Clean Architecture y Domain Driven Design en ASP.NET Core 8 avanzado**, y complementado con apuntes propios.
Para poder realizarlo, es necesario tener claro el curso anterior: **Clean Architecture y Domain Driven Design en ASP.NET Core 8**

---

# Índice completo de contenidos 📋
1. **[Strong Ids](#Seccion_01_StrongIds)**

 
---


## Agradecimientos 🎁

* Plataforma de aprendizaje online [Udemy](https://www.udemy.com/share/109PRS3@gz4ZDXhSu8i9pa_CnjiahHDgwCptf9vw-CYR0FqedgI2UGsgwy4nmPTe3ehw5QaGMA==/)
* A cualquiera que me invite a una cerveza 🍺.

---

# SECCIÓN 01. Clean architecture en .NET <a name="Seccion_01_StrongIds"></a>

**¿Qué son los strong ids?:**
* En el contexto del Domain-Driven Design (DDD), los Strong IDs (identificadores fuertes) son una técnica para gestionar identificadores de entidades de manera segura y expresiva.
* En resumen, en vez de utilizar `Guid`, utilizaremos `UserId`, `AlquilerId`, etcétera.

**¿Qué implicaciones tiene?:**
* Al utilizar strong ids, la generación no es directa. Esto implica, por ejemplo, tener que crear una interfaz `IEntity` para poderla utilizar en la clase `ApplicationDbContext`.

---
