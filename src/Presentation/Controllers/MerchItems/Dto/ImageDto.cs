namespace Contracts.Images;
public record struct ImageDto(Guid Id,
                              bool IsMain,
                              string Url);
