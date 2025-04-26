# Hash Table Performance Analysis

## Overview
This project evaluates the performance of **three types of hash tables** in C#:
- **Separate Chaining** (using linked lists)
- **Open Addressing** (Linear Probing)
- **Open Addressing** (Double Hashing)

We analyze:
✔ Collision rates  
✔ Insert/search execution times  
✔ Performance under different load factors  

---

## 📊 Experimental Results
![image](https://github.com/user-attachments/assets/c6b3b14b-853c-4369-a2bb-56bd445ec14e)

---

## 🔍 Observations & Analysis

### 🚀 **1. Chaining - High Collisions but Fast Execution**
- **Insertion (72ms)** is fast because collisions do **not require probing**, just appending to linked lists.
- **Collisions (36,811)** suggest uneven hash distribution, meaning many buckets are overloaded.
- **Search (21ms)** remains efficient since chains are short.

### ⚠ **2. Linear Probing - Slowed by Clustering**
- **Insertion (1,058ms)** is much slower due to excessive collision-based probing.
- **Collisions (15,027,738)** indicate severe **primary clustering**, causing extended search paths.
- **Search (234ms)** is slower than chaining since probing adds lookup overhead.

### ❌ **3. Double Hashing - Excessive Collisions**
- **Insertion (2,168ms)** is **unexpectedly slow**, likely due to a bad secondary hash function.
- **Collisions (40,803,175)** are **far too high**, suggesting poor step sizes leading to excessive probing.
- **Search (5ms)** is oddly fast—could be due to reduced lookup steps after insertion completes.

---

## 🔧 Recommended Fixes

### ✅ **Improve Hash Function**
A poor hash function causes imbalance in table distribution. Consider:
- **MurmurHash or FNV-1a** to spread keys more evenly.
- Avoid **modulo small primes**, as they often cause clustering.

### ✅ **Optimize Load Factor Strategy**
- Resize **earlier** at **75% capacity** to reduce probing intensity.
- Monitor **distribution of keys per bucket** to detect uneven spreads.

### ✅ **Validate Collision Counting**
- Ensure collisions are counted **only when an occupied slot prevents immediate insertion**.
- Log hash index distribution to detect key concentration issues.

---
