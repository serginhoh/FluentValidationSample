using FluentValidationSample.Models;
using FluentValidation;
using FluentValidation.TestHelper;

namespace FluentValidationSample.Validators.Tests
{
    public class PersonValidatorTests : IClassFixture<PersonValidator>
    {
        private readonly IValidator<Person> _validator;

        public PersonValidatorTests(PersonValidator validator)
        {
            _validator = validator;
        }

        [Fact()]
        public void PersonValidator_Sem_Erros()
        {
            //Arrange
            Person person = new() { Name = "Nome Teste", BirthDate = new DateTime(2000, 1, 1) };

            //Act
            var result = _validator.TestValidate(person);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void PersonValidator_Deve_Retornar_Erro_Nome_Null()
        {
            //arrange
            Person person = new() { Name = null, BirthDate = new DateTime(2000, 1, 1) };

            //act
            var result = _validator.TestValidate(person);

            //assert
            result.ShouldHaveValidationErrorFor(i => i.Name);
        }

        [Fact]
        public void PersonValidator_Deve_Retornar_Erro_Nome_Vazio()
        {
            //arrange
            Person person = new() { Name = "", BirthDate = new DateTime(2000, 1, 1) };

            //act
            var result = _validator.TestValidate(person);

            //assert
            result.ShouldHaveValidationErrorFor(i => i.Name);
        }

        [Fact]
        public void PersonValidator_Deve_Retornar_Erro_Menor_18_Anos()
        {
            //arrange
            Person person = new() { Name = "Nome Ok", BirthDate = DateTime.Now };

            //act
            var result = _validator.TestValidate(person);

            //assert
            result.ShouldHaveValidationErrorFor(i => i.BirthDate);
        }
    }
}