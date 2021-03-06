# DmiProperty.Processor.ProcessorId property

Gets a value representing the key to retrieve the property value.

Raw processor identification data.

The Processor ID field contains processor-specific information that describes the processor’s features.

* **x86** – The field’s format depends on the processor’s support of the CPUID instruction. If the instruction is supported, the Processor ID field contains two DWORD-formatted values.
* The first (offsets 08h-0Bh) is the EAX value returned by a CPUID instruction with input EAX set to 1; the second(offsets 0Ch-0Fh) is the EDX value returned by that instruction.
* **ARM32** – The processor ID field contains two DWORD-formatted values. The first (offsets 08h-0Bh) is the contents of the Main ID Register(MIDR); the second(offsets 0Ch-0Fh) is zero.
* **ARM64** – The processor ID field contains two DWORD-formatted values. The first (offsets 08h-0Bh) is the contents of the MIDR_EL1 register; the second (offsets 0Ch-0Fh) is zero.
* **RISC-V** – The processor ID contains a QWORD Machine Vendor ID CSR (mvendroid) of RISC-V processor hart 0. More information of RISC-V class CPU feature is described in RISC-V processor additional information.

Key Composition

* Structure: Processor
* Property: ProcessorId
* Unit: None

Return Value

Type: String

Remarks

2.0+

```csharp
public static IPropertyKey ProcessorId { get; }
```

## See Also

* class [Processor](../DmiProperty.Processor.md)
* namespace [iTin.Hardware.Specification.Dmi.Property](../../iTin.Hardware.Specification.Dmi.md)

<!-- DO NOT EDIT: generated by xmldocmd for iTin.Hardware.Specification.Dmi.dll -->
