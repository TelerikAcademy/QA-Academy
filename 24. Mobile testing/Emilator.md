Android Emulators
=======================
**List**

List installed SDK components:
```
android list sdk
```

List installed targets:
```
android list targets
```

List available emulators
```
android list avds
```

**Create Emulator**
```
android create avd -n Emulator-Api21-Default -t android-21 --abi default/x86 -f
```

**Run Emulator**
```
emulator -avd Emulator-Api21-Default -wipe-data -scale 0.5
```

**Adb**

[Adb tutorial](http://adbshell.com/commands/adb-install)
