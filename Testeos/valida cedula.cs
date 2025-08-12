using NUnit.Framework;
using VentaPro.Utils;

namespace Testeos
{
    [TestFixture]
    public class valida_cedula
    {
        [Test]
        public void Cedula_Valida_DeberiaRetornarTrue()
        {
            // Cédula de ejemplo válida según el algoritmo
            string cedulaValida = "1710034065";

            bool resultado = CedulaValidator.EsCedulaValida(cedulaValida);

            Assert.IsTrue(resultado, "La cédula debería ser válida.");
        }

        [Test]
        public void Cedula_Vacia_DeberiaRetornarFalse()
        {
            string cedula = "";

            bool resultado = CedulaValidator.EsCedulaValida(cedula);

            Assert.IsFalse(resultado, "Una cédula vacía no debería ser válida.");
        }

        [Test]
        public void Cedula_MenosDe10Digitos_DeberiaRetornarFalse()
        {
            string cedula = "123456";

            bool resultado = CedulaValidator.EsCedulaValida(cedula);

            Assert.IsFalse(resultado, "Una cédula con menos de 10 dígitos no debería ser válida.");
        }

        [Test]
        public void Cedula_ConLetras_DeberiaRetornarFalse()
        {
            string cedula = "17A0034065";

            bool resultado = CedulaValidator.EsCedulaValida(cedula);

            Assert.IsFalse(resultado, "Una cédula con letras no debería ser válida.");
        }

        [Test]
        public void Cedula_ConProvinciaInvalida_DeberiaRetornarFalse()
        {
            string cedula = "9910034065";

            bool resultado = CedulaValidator.EsCedulaValida(cedula);

            Assert.IsFalse(resultado, "Una cédula con provincia inválida no debería ser válida.");
        }
    }
}
