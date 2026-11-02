using System;

namespace DVLD.Domain.Common
{
    public static class EnumValidator
    {
        #region Private Helpers

        private static object ConvertToEnumUnderlyingType<TEnum>(
            object value,
            string? paramName)
            where TEnum : struct, Enum
        {
            var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));

            object convertedValue;

            try
            {
                convertedValue = Convert.ChangeType(
                    value,
                    underlyingType
                )!;
            }
            catch (Exception ex) when (
                ex is OverflowException ||
                ex is InvalidCastException ||
                ex is FormatException)
            {
                throw new ArgumentOutOfRangeException(
                    paramName ?? nameof(value),
                    value,
                    $"Invalid value for {typeof(TEnum).Name}.");
            }

            if (!Enum.IsDefined(typeof(TEnum), convertedValue))
            {
                throw new ArgumentOutOfRangeException(
                    paramName ?? nameof(value),
                    value,
                    $"Invalid {typeof(TEnum).Name} ID.");
            }

            return convertedValue;
        }

        private static void ValidateEnumValue<TEnum>(
            TEnum value,
            string? paramName)
            where TEnum : struct, Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
            {
                throw new ArgumentOutOfRangeException(
                    paramName ?? nameof(value),
                    value,
                    $"Invalid {typeof(TEnum).Name} value.");
            }
        }

        #endregion

        #region Integer Enum Validation

        public static int GetValidatedIntId<TEnum>(
            int id,
            string? paramName = null)
            where TEnum : struct, Enum
        {
            ConvertToEnumUnderlyingType<TEnum>(
                id,
                paramName ?? nameof(id)
            );

            return id;
        }

        public static int GetValidatedIntId<TEnum>(
            TEnum value,
            string? paramName = null)
            where TEnum : struct, Enum
        {
            ValidateEnumValue(value, paramName);

            try
            {
                return Convert.ToInt32(value);
            }
            catch (OverflowException)
            {
                throw new ArgumentOutOfRangeException(
                    paramName ?? nameof(value),
                    value,
                    $"Value cannot be represented as Int32.");
            }
        }

        #endregion

        #region Byte Enum Validation

        public static byte GetValidatedByteId<TEnum>(
            byte id,
            string? paramName = null)
            where TEnum : struct, Enum
        {
            ConvertToEnumUnderlyingType<TEnum>(
                id,
                paramName ?? nameof(id)
            );

            return id;
        }

        public static byte GetValidatedByteId<TEnum>(
            TEnum value,
            string? paramName = null)
            where TEnum : struct, Enum
        {
            ValidateEnumValue(value, paramName);

            try
            {
                return Convert.ToByte(value);
            }
            catch (OverflowException)
            {
                throw new ArgumentOutOfRangeException(
                    paramName ?? nameof(value),
                    value,
                    "Value cannot be represented as Byte.");
            }
        }

        #endregion
    }
}