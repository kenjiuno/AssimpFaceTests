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

        [Test]
        [TestCase("Triangle.fbx", 3)]
        public void Triangulate(string fbxFile, int indexCount)
        {
            var fbxPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, fbxFile);

            var assimp = new Assimp.AssimpContext();
            var scene = assimp.ImportFile(fbxPath, Assimp.PostProcessSteps.Triangulate);

            var inputMesh = scene.Meshes.Single();
            var face = inputMesh.Faces.Single();
            Assert.That(face.IndexCount, Is.EqualTo(indexCount));
        }

        [Test]
        [TestCase("Quad.fbx", 2)]
        [TestCase("Concave.fbx", 2)]
        [TestCase("Hex.fbx", 4)]
        [TestCase("HexBended.fbx", 4)]
        [TestCase("QuadBended.fbx", 2)]
        [TestCase("Poly6.fbx", 4)]
        public void TriangulateMultiFaces(string fbxFile, int facesCount)
        {
            var fbxPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, fbxFile);

            var assimp = new Assimp.AssimpContext();
            var scene = assimp.ImportFile(fbxPath, Assimp.PostProcessSteps.Triangulate);

            var inputMesh = scene.Meshes.Single();
            Assert.That(inputMesh.Faces.Count, Is.EqualTo(facesCount));

            // Each face must point JUST 3 coords (must be single triangle).
            Assert.That(
                inputMesh.Faces.Select(it => it.IndexCount).ToArray(),
                Is.EqualTo(
                    Enumerable.Repeat(3, inputMesh.Faces.Count).ToArray()
                )
            );
        }

    }
}