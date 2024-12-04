using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KnapsackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnapsackController : ControllerBase
    {
        [HttpPost("optimize-storage")]
        public IActionResult OptimizeStorage([FromBody] StorageRequest request)
        {
            if (request == null || request.Items == null || request.Capacity <= 0)
            {
                return BadRequest("Invalid input data.");
            }

            // Apply the knapsack algorithm
            var selectedItems = Knapsack(request.Capacity, request.Items);

            return Ok(selectedItems);
        }

        private List<Item> Knapsack(int capacity, List<Item> items)
        {
            int n = items.Count;
            int[,] dp = new int[n + 1, capacity + 1];

            for (int i = 1; i <= n; i++)
            {
                for (int w = 1; w <= capacity; w++)
                {
                    if (items[i - 1].Weight <= w)
                    {
                        dp[i, w] = Math.Max(
                            items[i - 1].Value + dp[i - 1, w - items[i - 1].Weight],
                            dp[i - 1, w]
                        );
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }

            List<Item> selectedItems = new List<Item>();
            int remainingCapacity = capacity;

            for (int i = n; i > 0 && remainingCapacity > 0; i--)
            {
                if (dp[i, remainingCapacity] != dp[i - 1, remainingCapacity])
                {
                    selectedItems.Add(items[i - 1]);
                    remainingCapacity -= items[i - 1].Weight;
                }
            }

            return selectedItems;
        }
    }

    public class StorageRequest
    {
        public int Capacity { get; set; }
        public List<Item>? Items { get; set; } = new List<Item>();
    }

    public class Item
    {
        public string? Name { get; set; } = string.Empty;
        public int Weight { get; set; }
        public int Value { get; set; }
    }
}
