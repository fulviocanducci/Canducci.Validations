using System;
using System.ComponentModel.DataAnnotations;
using Canducci.Validations.Attributes;

namespace Canducci.Validation.TestProject
{
    public class Tests
    {
        #region DateOrOptionalAttribute Tests

        [Test]
        public void DateOrOptionalAttribute_WithNullValue_ShouldReturnSuccess()
        {
            // Arrange
            var attribute = new DateOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var result = attribute.GetValidationResult(null, validationContext);

            // Assert
            Assert.AreEqual(ValidationResult.Success, result);
        }

        [Test]
        public void DateOrOptionalAttribute_WithDateTimeValue_ShouldReturnSuccess()
        {
            // Arrange
            var attribute = new DateOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.IsValid(new DateTime(2023, 12, 25));

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DateOrOptionalAttribute_WithDateOnlyValue_ShouldReturnSuccess()
        {
            // Arrange
            var attribute = new DateOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
#if NET6_0_OR_GREATER
            var result = attribute.IsValid(new DateOnly(2023, 12, 25));
            Assert.IsTrue(result);
#else
            // Skip test for older .NET versions
            Assert.IsTrue(true, "DateOnly not available in this .NET version");
#endif
        }

        [Test]
        public void DateOrOptionalAttribute_WithInvalidStringValue_ShouldReturnValidationError()
        {
            // Arrange
            var attribute = new DateOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.IsValid("invalid-date");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void DateOrOptionalAttribute_WithCustomFormats_ShouldUseCustomFormats()
        {
            // Arrange
            var attribute = new DateOrOptionalAttribute("DD-MM-YYYY", "MM/DD/YYYY");
            
            // Act & Assert
            Assert.AreEqual(new[] { "DD-MM-YYYY", "MM/DD/YYYY" }, attribute.Formats);
        }

        [Test]
        public void DateOrOptionalAttribute_WithDefaultConstructor_ShouldUseDefaultFormats()
        {
            // Arrange
            var attribute = new DateOrOptionalAttribute();
            
            // Act & Assert
            Assert.AreEqual(new[] { "DD/MM/YYYY", "YYYY-MM-DD" }, attribute.Formats);
        }

        [Test]
        public void DateOrOptionalAttribute_WithCustomErrorMessage_ShouldReturnCustomMessage()
        {
            // Arrange
            var attribute = new DateOrOptionalAttribute { ErrorMessage = "Data personalizada inválida" };
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.GetValidationResult("invalid", validationContext);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Data personalizada inválida", result.ErrorMessage);
        }

        [Test]
        public void DateOrOptionalAttribute_WithEmptyString_ShouldReturnValidationError()
        {
            // Arrange
            var attribute = new DateOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.IsValid("");

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region DateTimeOrOptionalAttribute Tests

        [Test]
        public void DateTimeOrOptionalAttribute_WithNullValue_ShouldReturnSuccess()
        {
            // Arrange
            var attribute = new DateTimeOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.GetValidationResult(null, validationContext);

            // Assert
            Assert.AreEqual(ValidationResult.Success, result);
        }

        [Test]
        public void DateTimeOrOptionalAttribute_WithDateTimeValue_ShouldReturnSuccess()
        {
            // Arrange
            var attribute = new DateTimeOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.IsValid(new DateTime(2023, 12, 25, 15, 30, 45));

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DateTimeOrOptionalAttribute_WithInvalidStringValue_ShouldReturnValidationError()
        {
            // Arrange
            var attribute = new DateTimeOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.IsValid("invalid-datetime");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void DateTimeOrOptionalAttribute_WithCustomFormats_ShouldUseCustomFormats()
        {
            // Arrange
            var attribute = new DateTimeOrOptionalAttribute("DD-MM-YYYY HH:mm", "MM/DD/YYYY HH:mm:ss");

            // Act & Assert
            Assert.AreEqual(new[] { "DD-MM-YYYY HH:mm", "MM/DD/YYYY HH:mm:ss" }, attribute.Formats);
        }

        [Test]
        public void DateTimeOrOptionalAttribute_WithDefaultConstructor_ShouldUseDefaultFormats()
        {
            // Arrange
            var attribute = new DateTimeOrOptionalAttribute();

            // Act & Assert
            Assert.AreEqual(new[] { "DD/MM/YYYY", "DD/MM/YYYY HH:mm", "DD/MM/YYYY HH:mm:ss", "YYYY-MM-DD", "YYYY-MM-DD HH:mm", "YYYY-MM-DD HH:mm:ss" }, attribute.Formats);
        }

        [Test]
        public void DateTimeOrOptionalAttribute_WithCustomErrorMessage_ShouldReturnCustomMessage()
        {
            // Arrange
            var attribute = new DateTimeOrOptionalAttribute { ErrorMessage = "Data/Hora personalizada inválida" };
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.GetValidationResult("invalid", validationContext);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Data/Hora personalizada inválida", result.ErrorMessage);
        }

        [Test]
        public void DateTimeOrOptionalAttribute_WithEmptyString_ShouldReturnValidationError()
        {
            // Arrange
            var attribute = new DateTimeOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.IsValid("");

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region TimeOrOptionalAttribute Tests

        [Test]
        public void TimeOrOptionalAttribute_WithNullValue_ShouldReturnSuccess()
        {
            // Arrange
            var attribute = new TimeOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.GetValidationResult(null, validationContext);

            // Assert
            Assert.AreEqual(ValidationResult.Success, result);
        }

        [Test]
        public void TimeOrOptionalAttribute_WithTimeSpanValue_ShouldReturnSuccess()
        {
            // Arrange
            var attribute = new TimeOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.IsValid(new TimeSpan(14, 30, 45));

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TimeOrOptionalAttribute_WithTimeOnlyValue_ShouldReturnSuccess()
        {
            // Arrange
            var attribute = new TimeOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
#if NET6_0_OR_GREATER
            var result = attribute.IsValid(new TimeOnly(14, 30, 45));
            Assert.IsTrue(result);
#else
            // Skip test for older .NET versions
            Assert.IsTrue(true, "TimeOnly not available in this .NET version");
#endif
        }

        [Test]
        public void TimeOrOptionalAttribute_WithInvalidStringValue_ShouldReturnValidationError()
        {
            // Arrange
            var attribute = new TimeOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.IsValid("invalid-time");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void TimeOrOptionalAttribute_WithCustomFormats_ShouldUseCustomFormats()
        {
            // Arrange
            var attribute = new TimeOrOptionalAttribute("HH:mm:ss", "HH:mm");

            // Act & Assert
            Assert.AreEqual(new[] { "HH:mm:ss", "HH:mm" }, attribute.Formats);
        }

        [Test]
        public void TimeOrOptionalAttribute_WithDefaultConstructor_ShouldUseDefaultFormats()
        {
            // Arrange
            var attribute = new TimeOrOptionalAttribute();

            // Act & Assert
            Assert.AreEqual(new[] { "HH:mm", "HH:mm:ss" }, attribute.Formats);
        }

        [Test]
        public void TimeOrOptionalAttribute_WithCustomErrorMessage_ShouldReturnCustomMessage()
        {
            // Arrange
            var attribute = new TimeOrOptionalAttribute { ErrorMessage = "Hora personalizada inválida" };
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.GetValidationResult("invalid", validationContext);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Hora personalizada inválida", result.ErrorMessage);
        }

        [Test]
        public void TimeOrOptionalAttribute_WithEmptyString_ShouldReturnValidationError()
        {
            // Arrange
            var attribute = new TimeOrOptionalAttribute();
            var validationContext = new ValidationContext(new { });

            // Act
            var result = attribute.IsValid("");

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region Integration Tests

        [Test]
        public void DateOrOptionalAttribute_ValidationContext_WithValidModel_ShouldPass()
        {
            // Arrange
            var model = new TestDateModel { OptionalDate = null };
            var validationContext = new ValidationContext(model);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        [Test]
        public void DateTimeOrOptionalAttribute_ValidationContext_WithValidModel_ShouldPass()
        {
            // Arrange
            var model = new TestDateTimeModel { OptionalDateTime = new DateTime(2023, 12, 25) };
            var validationContext = new ValidationContext(model);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        [Test]
        public void TimeOrOptionalAttribute_ValidationContext_WithValidModel_ShouldPass()
        {
            // Arrange
            var model = new TestTimeModel { OptionalTime = new TimeSpan(14, 30, 45) };
            var validationContext = new ValidationContext(model);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isValid);
            Assert.IsEmpty(validationResults);
        }

        #endregion

        #region Test Models

        private class TestDateModel
        {
            [DateOrOptional]
            public DateTime? OptionalDate { get; set; }
        }

        private class TestDateTimeModel
        {
            [DateTimeOrOptional]
            public DateTime? OptionalDateTime { get; set; }
        }

        private class TestTimeModel
        {
            [TimeOrOptional]
            public TimeSpan? OptionalTime { get; set; }
        }

        #endregion
    }
}