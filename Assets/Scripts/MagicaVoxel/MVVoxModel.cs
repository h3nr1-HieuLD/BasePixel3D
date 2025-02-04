using UnityEngine;

public class MVVoxModel : MonoBehaviour
{
	[HideInInspector]
	public bool ed_importAsIndividualVoxels;

	public string ed_filePath = "";

	[HideInInspector]
	public MVMainChunk vox;

	[Range(0.01f, 5f)]
	public float sizePerVox = 1f;

	public Material voxMaterial;

	public Transform meshOrigin;

	[Tooltip("If the vox file contains a palette, should it be converted to a texture?")]
	public bool paletteToTex;

	public bool reimportOnStart = true;

	public MVVoxModel()
	{
	}

	public void ClearVoxMeshes()
	{
		int i;
		MVVoxModelMesh[] componentsInChildren = base.gameObject.GetComponentsInChildren<MVVoxModelMesh>();
		for (i = 0; i < (int)componentsInChildren.Length; i++)
		{
			UnityEngine.Object.DestroyImmediate(componentsInChildren[i].gameObject);
		}
		MVVoxModelVoxel[] mVVoxModelVoxelArray = base.gameObject.GetComponentsInChildren<MVVoxModelVoxel>();
		for (i = 0; i < (int)mVVoxModelVoxelArray.Length; i++)
		{
			UnityEngine.Object.DestroyImmediate(mVVoxModelVoxelArray[i].gameObject);
		}
	}

	public void LoadVOXData(byte[] data, bool asIndividualVoxels)
	{
		this.ClearVoxMeshes();
		MVMainChunk mVMainChunk = MVImporter.LoadVOXFromData(data, true);
		if (mVMainChunk != null)
		{
			Material material = (this.voxMaterial != null ? this.voxMaterial : MVImporter.DefaultMaterial);
			if (this.paletteToTex)
			{
				material.mainTexture = (mVMainChunk.PaletteToTexture());
			}
			if (asIndividualVoxels)
			{
				MVImporter.CreateIndividualVoxelGameObjects(mVMainChunk, base.gameObject.transform, material, this.sizePerVox);
			}
			else
			{
				MVImporter.CreateVoxelGameObjects(mVMainChunk, base.gameObject.transform, material, this.sizePerVox);
			}
			this.vox = mVMainChunk;
		}
	}

	public void LoadVOXFile(string path, bool asIndividualVoxels)
	{
		this.ClearVoxMeshes();
		if (path == null || path.Length <= 0)
		{
			Debug.LogError("[MVVoxModel] Invalid file path");
		}
		else
		{
			MVMainChunk mVMainChunk = MVImporter.LoadVOX(path, true);
			if (mVMainChunk != null)
			{
				Material material = (this.voxMaterial != null ? this.voxMaterial : MVImporter.DefaultMaterial);
				if (this.paletteToTex)
				{
					material.mainTexture = (mVMainChunk.PaletteToTexture());
				}
				if (!asIndividualVoxels)
				{
					if (this.meshOrigin == null)
					{
						MVImporter.CreateVoxelGameObjects(mVMainChunk, base.gameObject.transform, material, this.sizePerVox);
					}
					else
					{
						MVImporter.CreateVoxelGameObjects(mVMainChunk, base.gameObject.transform, material, this.sizePerVox, this.meshOrigin.localPosition);
					}
				}
				else if (this.meshOrigin == null)
				{
					MVImporter.CreateIndividualVoxelGameObjects(mVMainChunk, base.gameObject.transform, material, this.sizePerVox);
				}
				else
				{
					MVImporter.CreateIndividualVoxelGameObjects(mVMainChunk, base.gameObject.transform, material, this.sizePerVox, this.meshOrigin.localPosition);
				}
				this.vox = mVMainChunk;
				return;
			}
		}
	}

	private void Start()
	{
		if (this.reimportOnStart)
		{
			this.LoadVOXFile(this.ed_filePath, this.ed_importAsIndividualVoxels);
		}
	}
}