using NUnit.Framework;

namespace AssimpFaceTests
{
    public class Class1
    {
        [Test]
        [TestCase("Triangle.fbx", 3)]
        [TestCase("Quad.fbx", 4)]
        [TestCase("Concave.fbx", 4)]
        [TestCase("Hex.fbx", 6)]
        [TestCase("HexBended.fbx", 6)]
        [TestCase("QuadBended.fbx", 4)]
        [TestCase("Poly6.fbx", 6)]
        public void Single(string fbxFile, int indexCount)
        {
            var fbxPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, fbxFile);

            var assimp = new Assimp.AssimpContext();
            var scene = assimp.ImportFile(fbxPath, Assimp.PostProcessSteps.PreTransformVertices);

            var inputMesh = scene.Meshes.Single();
            var face = inputMesh.Faces.Single();
            Assert.That(face.IndexCount, Is.EqualTo(indexCount));
        }

        [Test]
        [TestCase("Bended2Tris.fbx")]
        public void Two(string fbxFile)
        {
            var fbxPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, fbxFile);

            var assimp = new Assimp.AssimpContext();
            var scene = assimp.ImportFile(fbxPath, Assimp.PostProcessSteps.PreTransformVertices);

            var inputMesh = scene.Meshes.Single();
            Assert.That(inputMesh.Faces.Count, Is.EqualTo(2));
        }
    }
}