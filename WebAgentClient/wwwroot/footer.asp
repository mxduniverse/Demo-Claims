

  <!-- credits -->
  <div class="text-center mt-4">
    <p>
      Assurance Agent Web Application
    </p>

  </div>
  <script>


          $(function () {
      var availableTags = [
        "House", "car", "laptop", "phone", "printer", "Bike", "MTB Bike", "Shoes"
      ];

      var IncidenceTags = [
        "Food", "tornado", "thunder","flood", "theft"
      ];
      $("#damagedItem").autocomplete({
        source: availableTags
      });
      $("#incidence").autocomplete({
        source: IncidenceTags
      });

    });

  </script>
</body>
</html>

