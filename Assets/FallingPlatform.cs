using System.Collections; // Necessário para corrotinas
using UnityEngine;
using UnityEngine.Tilemaps; // Necessário para trabalhar com Tilemap

public class FallingTilemap : MonoBehaviour
{
    public float fallDelay = 1f; // Tempo antes de o tile cair
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtém a posição do contato
            Vector3 hitPosition = Vector3.zero;

            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition = hit.point;
                break;
            }

            // Converte para coordenadas do Tilemap
            Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);

            // Inicia a queda do tile após o delay
            StartCoroutine(FallTile(cellPosition));
        }
    }

    private IEnumerator FallTile(Vector3Int cellPosition)
    {
        yield return new WaitForSeconds(fallDelay);

        // Verifica se o tile ainda existe na posição
        if (tilemap.HasTile(cellPosition))
        {
            // Remove o tile para simular a queda
            tilemap.SetTile(cellPosition, null);
        }
    }
}
