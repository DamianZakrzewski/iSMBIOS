# DmiProperty.MemoryDevice.Speed property

Gets a value representing the key to retrieve the property.

Identifies the maximum capable speed of the device, in megatransfers per second(MT/s). 0000h = the speed is unknown. FFFFh = the speed is 65,535 MT/s or greater, and the actual speed is stored in the [`ExtendedSpeed`](ExtendedSpeed.md) property.

Key Composition

* Structure: MemoryDevice
* Property: Speed
* Unit: MTs

Return Value

Type: UInt16

Remarks

2.3+

```csharp
public static IPropertyKey Speed { get; }
```

## See Also

* class [MemoryDevice](../DmiProperty.MemoryDevice.md)
* namespace [iTin.Hardware.Specification.Dmi.Property](../../iTin.Hardware.Specification.Dmi.md)

<!-- DO NOT EDIT: generated by xmldocmd for iTin.Hardware.Specification.Dmi.dll -->